// Copyright (c) RaaLabs. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.


namespace RaaLabs.Edge.Connectors.NMEA
{
    /// <summary>
    /// The protocol to use for connections
    /// </summary>
    public enum Protocol
    {
        /// <summary>
        /// Straight NMEA TCP
        /// </summary>
        Tcp = 1,

        /// <summary>
        /// Straight NMEA UDP 
        /// </summary>
        Udp
    }
}