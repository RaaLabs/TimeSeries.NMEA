using BoDi;
using RaaLabs.Edge.Modules.EventHandling;
using RaaLabs.Edge.Connectors.NMEA.Specs.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TechTalk.SpecFlow;
using FluentAssertions;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace RaaLabs.Edge.Connectors.NMEA.Specs.Steps
{
    [Binding]
    public sealed class NMEAStreamReaderSteps
    {
        private readonly IObjectContainer _container;
        private NMEAStreamReader _streamReader;
        private Stream _stream;

        public NMEAStreamReaderSteps(IObjectContainer container, NMEAStreamReader streamReader)
        {
            _container = container;
            _streamReader = streamReader;
        }

        [When(@"tcp stream ""(.*)"" is received")]
        public void WhenTcpStreamIsReceived(string sentence)
        {
            sentence = sentence.Replace("\\n", "\n");
            _stream = new MemoryStream(Encoding.ASCII.GetBytes(sentence));
        }

        [Then(@"something")]
        public async Task ThenSomething()
        {
            // TODO
            var reader = NMEAStreamReader.ReadLineAsync(_stream).GetAsyncEnumerator();
            while (await DoWithTimeout(reader.MoveNextAsync(), 10))
            {
                var line = reader.Current;
                Console.WriteLine(line);
            }

        }

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




        /*private async string ReadLine()
        {
            await _reader.MoveNextAsync()

        }*/

    }
}
