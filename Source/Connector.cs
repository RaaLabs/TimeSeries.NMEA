// Copyright (c) RaaLabs. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Polly;
using RaaLabs.Edge.Modules.EventHandling;
using Serilog;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RaaLabs.Edge.Connectors.NMEA
{
    /// <summary>
    /// Represents a TCP or UDP connector type that connects and streams data from the source
    /// </summary>
    [ExcludeFromCodeCoverage] // Covered by integration tests
    public class Connector : IRunAsync, IProduceEvent<Events.NMEASentenceReceived>
    {
        /// <inheritdoc/>
        public event EventEmitter<Events.NMEASentenceReceived> NMEASentenceReceived;
        private readonly ConnectorConfiguration _configuration;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="Connector"/>
        /// </summary>
        /// <param name="configuration">The <see cref="ConnectorConfiguration">configuration</see></param>
        /// <param name="logger"><see cref="ILogger"/> for logging</param>
        public Connector(
            ConnectorConfiguration configuration,
            ILogger logger)
        {
            _configuration = configuration;
            _logger = logger;
            _logger.Information($"Using protocol: {_configuration.Protocol}");
        }

        /// <summary>
        /// Implmentation of <see cref="IRunAsync"/>
        /// </summary>
        /// <returns>
        /// NMEASentenceReceived <see cref="NMEASentenceReceived"/>
        /// </returns>
        public async Task Run()
        {
            while (true)
            {
                _logger.Information($"Setting up {_configuration.Protocol} connector");
                var policy = Policy
                    .Handle<Exception>()
                    .WaitAndRetryForeverAsync(retryAttempt => TimeSpan.FromSeconds(Math.Min(Math.Pow(2, retryAttempt),3600)),
                    (exception, timeSpan, context) =>
                    {
                        _logger.Error(exception, $"NMEA connector threw an exception during connect - retrying");
                    });

                switch (_configuration.Protocol)
                {
                    case Protocol.Tcp: await policy.ExecuteAsync(async () => { await TcpConnect(); }); break;
                    case Protocol.Udp: await policy.ExecuteAsync(async () => { await ConnectUdp(); }); break;
                    default: _logger.Error("Protocol not defined"); break;
                }
                await Task.Delay(1000);
            }
        }

        private async Task TcpConnect()
        {
            IPAddress address = IPAddress.Parse(_configuration.Ip);
            var port = _configuration.Port;

            Socket socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(address, port);

            using (var stream = new NetworkStream(socket, FileAccess.Read, true))
            {
                stream.ReadTimeout = 30_000;
                var reader = NMEAStreamReader.ReadLineAsync(stream).GetAsyncEnumerator();
                try
                {
                    while (true)
                    {
                        await DoWithTimeout(reader.MoveNextAsync(), 3_000);
                        var sentence = reader.Current;
                        NMEASentenceReceived(sentence);
                    }
                }
                finally
                {
                    _logger.Information("Done reading TCP stream");
                    socket.Close();
                }
            }
        }

        private async Task ConnectUdp()
        {
            while (true)
            {
                try
                {
                    using (var udpClient = new UdpClient(_configuration.Port))
                    {
                        try
                        {
                            while (true)
                            {
                                var receivedResult = await udpClient.ReceiveAsync();
                                var sentence = Encoding.ASCII.GetString(receivedResult.Buffer);
                                NMEASentenceReceived(sentence);
                            }
                        }
                        catch (SocketException ex)
                        {
                            _logger.Error(ex, $"Trouble connecting to socket");
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, "Error while connecting to UDP stream");
                    Thread.Sleep(2000);
                }
            }
        }

        /// <summary>
        /// Helper method that can handle timeout for ValueTasks.
        /// </summary>
        async ValueTask<T> DoWithTimeout<T>(ValueTask<T> valueTask, int timeout)
        {
            var task = valueTask.AsTask();
            if (await Task.WhenAny(task, Task.Delay(timeout)) == task)
            {
                return await task;
            }
            else
            {
                throw new OperationCanceledException();
            }
        }
    }
}