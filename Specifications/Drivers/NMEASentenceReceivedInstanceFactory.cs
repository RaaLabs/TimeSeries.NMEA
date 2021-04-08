using RaaLabs.Edge.Connectors.NMEA.events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

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
