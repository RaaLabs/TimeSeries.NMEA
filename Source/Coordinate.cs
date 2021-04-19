// Copyright (c) RaaLabs. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace RaaLabs.Edge.Connectors.NMEA
{
    /// <summary>
    /// Represents a physical coordinate
    /// </summary>
    public class Coordinate
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Coordinate"/>
        /// </summary>
        /// <param name="latitude"><see cref="Latitude"/> in decimals</param>
        /// <param name="longitude"><see cref="Longitude"/> in decimals</param>
        public Coordinate(float latitude, float longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        /// <summary>
        /// Gets or sets the location latitude in decimal degrees
        /// </summary>
        public float Latitude { get; set; }

        /// <summary>
        /// Gets or sets the location longitude in decimal degrees
        /// </summary>
        public float Longitude { get; set; }

    }
}
