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
            _logger.Information($"Setting up {_configuration.Protocol} connector");

            var retryOnClosedConnection = Policy
                .Handle<Exception>()
                .RetryForeverAsync(ex =>
                {
                    _logger.Error(ex, "NMEA connection threw exception. Restarting connection.");
                });

            var retryUntilConnected = Policy
                .Handle<SocketException>()
                .WaitAndRetryForeverAsync(retryAttempt => TimeSpan.FromSeconds(Math.Min(Math.Pow(2, retryAttempt), 3600)),
                (exception, retryAttempt, context) =>
                {
                    var nextRetry = Math.Min(Math.Pow(2, retryAttempt), 3600);
                    _logger.Error(exception, "NMEA connector threw an exception during connect - retrying in {Timespan} seconds", nextRetry);
                });

            var policy = Policy.WrapAsync(retryOnClosedConnection, retryUntilConnected);

            switch (_configuration.Protocol)
            {
                case Protocol.Tcp: await policy.ExecuteAsync(async () => { await TcpConnect(); }); break;
                case Protocol.Udp: await policy.ExecuteAsync(async () => { await ConnectUdp(); }); break;
                default: _logger.Error("Protocol not defined"); break;
            }
        }

        private async Task TcpConnect()
        {
            _logger.Information("Connecting to TCP stream");
            var address = IPAddress.Parse(_configuration.Ip);
            var port = _configuration.Port;

            var socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(address, port);

            using var stream = new NetworkStream(socket, FileAccess.Read, true);
            var reader = NMEAStreamReader.ReadLineAsync(stream, TimeSpan.FromSeconds(3));
            try
            {
                await foreach (var sentence in reader)
                {
                    NMEASentenceReceived(sentence);
                }
            }
            finally
            {
                _logger.Information("Done reading TCP stream");
                socket.Close();
            }
        }

        private async Task ConnectUdp()
        {
            using var udpClient = new UdpClient(_configuration.Port);

            while (true)
            {
                var receivedResult = await udpClient.ReceiveAsync();
                var sentence = Encoding.ASCII.GetString(receivedResult.Buffer);
                NMEASentenceReceived(sentence);
            }
        }
    }
}