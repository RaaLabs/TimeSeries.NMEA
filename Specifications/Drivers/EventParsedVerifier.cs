using RaaLabs.Edge.Connectors.NMEA.events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using FluentAssertions;
using System.Globalization;

namespace RaaLabs.Edge.Connectors.NMEA.Specs.Drivers
{
    class EventParsedVerifier : IProducedEventVerifier<EventParsed>
    {
        public void VerifyFromTableRow(EventParsed @event, TableRow row)
        {

            float actualValue = @event.value;

            @event.talker.Should().Be(row["Talker"]);
            @event.tag.Should().Be(row["Tag"]);
            actualValue.Should().BeApproximately(float.Parse(row["Value"], CultureInfo.InvariantCulture.NumberFormat), 0.0001f);
        }
    }
}

