using RaaLabs.Edge.Connectors.NMEA.events;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace RaaLabs.Edge.Connectors.NMEA.Specs.Drivers
{
    class EventParsedInstanceFactory : IEventInstanceFactory<EventParsed>
    {
        public EventParsed FromTableRow(TableRow row)
        {

            var eventParsed = new events.EventParsed
            {
                talker = row["Identifier"],
                tag = row["Tag"],
                timestamp = long.Parse(row["Timestamp"], CultureInfo.InvariantCulture.NumberFormat),
                value = float.Parse(row["Value"], CultureInfo.InvariantCulture.NumberFormat)
            };
            return eventParsed;
        }
    }
}
