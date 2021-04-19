using System.Globalization;
using TechTalk.SpecFlow;
using RaaLabs.Edge.Connectors.NMEA.Events;

namespace RaaLabs.Edge.Connectors.NMEA.Specs.Drivers
{
    class EventParsedInstanceFactory : IEventInstanceFactory<EventParsed>
    {
        public EventParsed FromTableRow(TableRow row)
        {
            var eventParsed = new Events.EventParsed
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
