// Copyright (c) RaaLabs. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Globalization;

namespace RaaLabs.Edge.Connectors.NMEA.SentenceFormats
{
    /// <summary> 
    /// Represents the format of "RMA - Recommended Minimum Navigation Information"
    /// </summary>
    public class RMA : ISentenceFormat
    {

        /// <inheritdoc/>
        public string Identitifer => "RMA";
        readonly Parser parser = new Parser();


        /// <inheritdoc/>
        public IEnumerable<TagWithData> Parse(string[] values)
        {
            var latitude = values[1];
            var longitude = values[3];
            var cardinalDirectionY = values[2];
            var cardinalDirectionX = values[4];
            var speedOverGround = values[7];

            if (parser.ValidSentenceValue(speedOverGround)) yield return new TagWithData("SpeedOverGround", parser.KnotsToMps(speedOverGround));

            var positionTags = parser.ParsePosition(latitude, longitude, cardinalDirectionX, cardinalDirectionY);
            foreach (var datapoint in positionTags)
            {
                yield return new TagWithData(datapoint.Tag, datapoint.Data);
            }
        }
    }
}