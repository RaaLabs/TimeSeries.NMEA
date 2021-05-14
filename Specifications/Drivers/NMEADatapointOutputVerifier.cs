using TechTalk.SpecFlow;
using FluentAssertions;
using System.Globalization;
using RaaLabs.Edge.Connectors.NMEA.Events;

namespace RaaLabs.Edge.Connectors.NMEA.Specs.Drivers
{
    class NMEADatapointOutputVerifier : IProducedEventVerifier<NMEADatapointOutput>
    {
        public void VerifyFromTableRow(NMEADatapointOutput @event, TableRow row)
        {
            float actualValue = @event.Value;
            var expectedValue = float.Parse(row["Value"], CultureInfo.InvariantCulture.NumberFormat);
            @event.Source.Should().Be("NMEA");
            @event.Tag.Should().Be(row["Tag"]);
            actualValue.Should().BeApproximately(expectedValue, 0.0001f);
        }
    }
}

