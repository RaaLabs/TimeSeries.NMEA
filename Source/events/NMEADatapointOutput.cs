// Copyright (c) RaaLabs. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using RaaLabs.Edge.Modules.EdgeHub;

namespace RaaLabs.Edge.Connectors.NMEA.events
{
    /// <summary>
    /// Implement outgoing event
    /// </summary>
    [OutputName("output")]
    public class NMEADatapointOutput : IEdgeHubOutgoingEvent
    {
        /// <summary>
        /// Represents the Source system.
        /// </summary>
        public string source { get; set; }

        /// <summary>
        /// Gets or sets the tag. Represens the sensor name from the source system.
        /// </summary>
        public string tag { get; set; }

        /// <summary>
        /// Gets or sets the value
        /// </summary>
        public dynamic value { get; set; }

        /// <summary>
        /// Gets or sets the timestamp in the form of EPOCH milliseconds granularity
        /// </summary>
        public long timestamp { get; set; }

    }
}
