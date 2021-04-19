using BoDi;
using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;

namespace RaaLabs.Edge.Connectors.NMEA.Specs.Hooks
{
    [Binding]
    class SentenceParserProvider
    {
        private readonly IObjectContainer _container;

        public SentenceParserProvider(IObjectContainer container)
        {
            _container = container;
        }

        [BeforeScenario]
        public void SetupSentenceParserProvider()
        {
            var assembly = typeof(ISentenceFormat).Assembly;
            var sentenceFormats = assembly.GetTypes().Where(type => typeof(ISentenceFormat).IsAssignableFrom(type) && !type.IsInterface);
            _container.RegisterFactoryAs<IEnumerable<ISentenceFormat>>(_ => sentenceFormats.Select(sentenceFormat => (ISentenceFormat) Activator.CreateInstance(sentenceFormat)) );
        }

        public void RegisterSentenceFormat<T>() where T : class, ISentenceFormat
        {
            _container.RegisterTypeAs<T, ISentenceFormat>();
        }
    }
}
