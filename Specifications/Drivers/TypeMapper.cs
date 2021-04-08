using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaaLabs.Edge.Connectors.NMEA;

namespace RaaLabs.Edge.Connectors.NMEA.Specs.Drivers
{
    public class TypeMapping : Dictionary<string, Type>
    {
        public TypeMapping()
        {
            Add("NMEALineHandler", typeof(NMEALineHandler));
            Add("NMEASentenceReceived", typeof(events.NMEASentenceReceived));
            Add("EventParsed", typeof(events.EventParsed));
            Add("NMEADatapointOutput", typeof(events.NMEADatapointOutput));
        }
    }
}
