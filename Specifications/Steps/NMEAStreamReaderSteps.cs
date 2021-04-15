using System;
using TechTalk.SpecFlow;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace RaaLabs.Edge.Connectors.NMEA.Specs.Steps
{
    [Binding]
    public sealed class NMEAStreamReaderSteps
    {
        private NMEAStreamReader _streamReader;
        private Stream _stream;

        public NMEAStreamReaderSteps(NMEAStreamReader streamReader)
        {
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
