using TechTalk.SpecFlow;
using FluentAssertions;
using System.Globalization;
using Newtonsoft.Json;
using RaaLabs.Edge.Connectors.NMEA.Events;

namespace RaaLabs.Edge.Connectors.NMEA.Specs.Drivers
{
    class EventParsedVerifier : IProducedEventVerifier<EventParsed>
    {
        public void VerifyFromTableRow(EventParsed @event, TableRow row)
        {
            if (@event.tag != "Position")
            {
                float actualValue = @event.value;
                var expectedValue = float.Parse(row["Value"], CultureInfo.InvariantCulture.NumberFormat);
                @event.talker.Should().Be(row["Talker"]);
                @event.tag.Should().Be(row["Tag"]);
                actualValue.Should().BeApproximately(expectedValue, 0.0001f);
            }
            else
            {
                var expectedCoordinate = JsonConvert.DeserializeObject<Coordinate>(row["Value"]);
                float latitude = @event.value.Latitude;
                float longitude = @event.value.Longitude;
                latitude.Should().BeApproximately(expectedCoordinate.Latitude, 0.0001f);
                longitude.Should().BeApproximately(expectedCoordinate.Longitude, 0.0001f);
            }
        }
    }
}

