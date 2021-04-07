// Copyright (c) RaaLabs. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;

namespace RaaLabs.Edge.Connectors.NMEA.SentenceFormats
{
    /// <summary> 
    /// Represents the format of "Recommended Minimum Navigation Information"
    /// </summary>
    public class RMC : ISentenceFormat
    {
        /// <inheritdoc/>
        public string Identitifer => "RMC";
        readonly Parser parser = new Parser();

        /// <inheritdoc/>
        public IEnumerable<TagWithData> Parse(string[] values)
        {
            var latitude = values[2];
            var longitude = values[4];
            var cardinalDirectionY = values[3];
            var cardinalDirectionX = values[5];
            var speedOverGround = values[6];

            if (parser.ValidSentenceValue(speedOverGround)) yield return new TagWithData("SpeedOverGround", parser.StringToDouble(speedOverGround) * 1852 / 3600);
            
            var positionTags = parser.ParsePosition(latitude, longitude, cardinalDirectionX, cardinalDirectionY);
            foreach (var datapoint in positionTags)
            {
                yield return new TagWithData(datapoint.Tag, datapoint.Data);
            }
        }
      

    }
}