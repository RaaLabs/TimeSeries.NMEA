// Copyright (c) RaaLabs. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Globalization;

namespace RaaLabs.Edge.Connectors.NMEA.SentenceFormats
{
    /// <summary> 
    /// Represents the format of "GGA - Global Positioning System Fix Data"
    /// </summary>
    public class GGA : ISentenceFormat
    {

        /// <inheritdoc/>
        public string Identitifer => "GGA";
        readonly Parser parser = new Parser();

        /// <inheritdoc/>
        public IEnumerable<TagWithData> Parse(string[] values)
        {
            var latitude = values[1];
            var longitude = values[3];
            var cardinalDirectionY = values[2];
            var cardinalDirectionX = values[4];
            var gpsSatellites = values[6];
            var hdop = values[7];

            if (parser.ValidSentenceValue(gpsSatellites)) yield return new TagWithData("GPSsatellites", parser.StringToDouble(gpsSatellites));
            if (parser.ValidSentenceValue(hdop)) yield return new TagWithData("HDOP", parser.StringToDouble(hdop));

            var positionTags = parser.ParsePosition(latitude, longitude, cardinalDirectionX, cardinalDirectionY);
            foreach (var datapoint in positionTags)
            {
                yield return new TagWithData(datapoint.Tag, datapoint.Data);
            }
        }
    }
}