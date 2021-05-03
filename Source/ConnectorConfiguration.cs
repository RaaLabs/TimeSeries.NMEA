// Copyright (c) RaaLabs. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using RaaLabs.Edge.Modules.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace RaaLabs.Edge.Connectors.NMEA
{
    /// <summary>
    /// Represents the configuration of the NMEA connector
    /// </summary>
    [ExcludeFromCodeCoverage] // Do not need to test configuration
    [Name("connector.json")]
    [RestartOnChange]
    public class ConnectorConfiguration : IConfiguration
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ConnectorConfiguration"/>
        /// </summary>
        /// <param name="ip">The IP address for the connector</param>
        /// <param name="port">The Port to connect to</param>
        /// <param name="protocol"><see cref="Protocol"/> to use for connecting</param>
        public ConnectorConfiguration(string ip, int port, Protocol protocol)
        {
            Ip = ip;
            Port = port;
            Protocol = protocol;
        }

        /// <summary>
        /// Gets the Ip address that will be used for connecting
        /// </summary>
        public string Ip { get; }

        /// <summary>
        /// Gets the port that will be used for connecting
        /// </summary>
        public int Port { get; }
        
        /// <summary>
        /// Gets the <see cref="Protocol"/> to use
        /// </summary>
        public Protocol Protocol { get; }
    }
}