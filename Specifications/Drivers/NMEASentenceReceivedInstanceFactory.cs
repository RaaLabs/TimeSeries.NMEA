using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using RaaLabs.Edge.Connectors.NMEA.Events;

namespace RaaLabs.Edge.Connectors.NMEA.Specs.Drivers
{
    class NMEASentenceReceivedInstanceFactory : IEventInstanceFactory<NMEASentenceReceived>
    {
        public NMEASentenceReceived FromTableRow(TableRow row)
        {
            var sentence = row.CreateInstance<NMEASentenceReceived>();
            return sentence;
        }
    }
}
