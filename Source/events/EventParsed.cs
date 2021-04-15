// Copyright (c) RaaLabs. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using RaaLabs.Edge.Modules.EventHandling;

namespace RaaLabs.Edge.Connectors.NMEA.Events
{
    /// <summary>
    /// Implements event for EventParsed. See https://gpsd.gitlab.io/gpsd/NMEA.html for more information
    /// </summary>
    public class EventParsed : IEvent
    {
        /// <summary>
        /// Represents the NMEA talker. 
        /// </summary>
        public string talker {get; set; }

        /// <summary>
        /// Represents the NMEA tag name
        /// </summary>
        public string tag { get; set; }

        /// <summary>
        /// Represents the value for the NMEA tag
        /// </summary>
        public dynamic  value { get; set; }

        /// <summary>
        /// Represents the timestamp for the NMEA tag in epoc
        /// </summary>
        public long timestamp { get; set; }
    }
}
