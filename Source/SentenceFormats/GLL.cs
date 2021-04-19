// Copyright (c) RaaLabs. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Globalization;

namespace RaaLabs.Edge.Connectors.NMEA.SentenceFormats
{
    /// <summary> 
    /// Represents the format of "Recommended Minimum Navigation Information"
    /// </summary>
    public class GLL : ISentenceFormat
    {

        /// <inheritdoc/>
        public string Identitifer => "GLL";
        readonly Parser parser = new Parser();

        /// <inheritdoc/>
        public IEnumerable<TagWithData> Parse(string[] values)
        {

            var latitude = values[0];
            var longitude = values[2];
            var cardinalDirectionY = values[1];
            var cardinalDirectionX = values[3];

            var positionTags = parser.ParsePosition(latitude, longitude, cardinalDirectionX, cardinalDirectionY);
            foreach (var datapoint in positionTags)
            {
                yield return new TagWithData(datapoint.Tag, datapoint.Data);
            }
        }
    }
}