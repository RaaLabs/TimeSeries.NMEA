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
                Talker = row["Identifier"],
                Tag = row["Tag"],
                Timestamp = long.Parse(row["Timestamp"], CultureInfo.InvariantCulture.NumberFormat),
                Value = float.Parse(row["Value"], CultureInfo.InvariantCulture.NumberFormat)
            };
            return eventParsed;
        }
    }
}
