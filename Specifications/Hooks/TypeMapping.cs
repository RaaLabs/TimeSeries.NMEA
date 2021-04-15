using TechTalk.SpecFlow;
using RaaLabs.Edge.Testing;

namespace RaaLabs.Edge.Connectors.NMEA.Specs.Drivers
{

    [Binding]
    public sealed class TypeMapperProvider
    {
        private readonly TypeMapping _typeMapping;

        public TypeMapperProvider(TypeMapping typeMapping)
        {
            _typeMapping = typeMapping;
        }

        [BeforeScenario]
        public void SetupTypes()
        {
            _typeMapping.Add("NMEALineHandler", typeof(NMEALineHandler));
            _typeMapping.Add("StateHandler", typeof(StateHandler));
            _typeMapping.Add("NMEASentenceReceived", typeof(Events.NMEASentenceReceived));
            _typeMapping.Add("EventParsed", typeof(Events.EventParsed));
            _typeMapping.Add("NMEADatapointOutput", typeof(Events.NMEADatapointOutput));
        }
    }
}


