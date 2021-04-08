using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace RaaLabs.Edge.Connectors.NMEA.Specs.Drivers
{
    interface IProducedEventVerifier<T>
    {
        public void VerifyFromTableRow(T @event, TableRow row);
    }
}
