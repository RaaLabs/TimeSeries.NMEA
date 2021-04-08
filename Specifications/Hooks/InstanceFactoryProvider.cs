using BoDi;
using RaaLabs.Edge.Connectors.NMEA.events;
using RaaLabs.Edge.Connectors.NMEA.Specs.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace RaaLabs.Edge.Connectors.NMEA.Specs.Hooks
{
    [Binding]
    class InstanceFactoryProvider
    {
        private readonly IObjectContainer _container;

        public InstanceFactoryProvider(IObjectContainer container)
        {
            _container = container;
        }

        [BeforeScenario]
        public void SetupIntanceFactory()
        {
            _container.RegisterTypeAs<NMEASentenceReceivedInstanceFactory, IEventInstanceFactory<NMEASentenceReceived>>();
            _container.RegisterTypeAs<EventParsedVerifier, IProducedEventVerifier<EventParsed>>();
        }

    }
}
