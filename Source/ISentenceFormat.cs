// Copyright (c) RaaLabs. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;

namespace RaaLabs.Edge.Connectors.NMEA
{

    /// <summary>
    /// Defines how a format for a sentence is to be parsed
    /// </summary>
    public interface ISentenceFormat
    {
        /// <summary>
        /// Gets the setence identifier 
        /// </summary>
        string Identitifer { get; }

        /// <summary>
        /// Parse the sentence values and return an instance of a type representing it
        /// </summary>
        /// <param name="values">The values to parse</param>
        /// <returns>All the results</returns>
        IEnumerable<TagWithData> Parse(string[] values);
    }
}