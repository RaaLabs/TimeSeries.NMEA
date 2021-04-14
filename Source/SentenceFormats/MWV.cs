// Copyright (c) RaaLabs. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Globalization;

namespace RaaLabs.Edge.Connectors.NMEA.SentenceFormats
{
    /// <summary>
    /// Represents the format of "Wind Speed and Angle"
    /// </summary>
    public class MWV : ISentenceFormat
    {

        /// <inheritdoc/>
        public string Identitifer => "MWV";
        readonly Parser parser = new Parser();


        /// <inheritdoc/>
        public IEnumerable<TagWithData> Parse(string[] values)
        {
            var windAngle = values[0];
            var reference = values[1];
            var windSpeed = values[2];
            var windSpeedUnit = values[3];

            var windAngleName = "WindAngleTrue";
            var windUnit = "WindSpeedTrue";
            if (reference == "R")
            {
                windAngleName = "WindAngleRelative";
                windUnit = "WindSpeedRelative";
            }

            if (parser.ValidSentenceValue(windAngle)) yield return new TagWithData(windAngleName, parser.StringToDouble(windAngle));
            if (parser.ValidSentenceValue(windSpeed))
            {
                
                float windSpeedValue;
                switch (windSpeedUnit)
                {
                    case "K": windSpeedValue = parser.KphToMps(windSpeed); break;
                    case "N": windSpeedValue = parser.KnotsToMps(windSpeed); break;
                    case "M": windSpeedValue = parser.StringToDouble(windSpeed);; break;
                    default: windSpeedValue = parser.StringToDouble(windSpeed); break;
                }
                yield return new TagWithData(windUnit, windSpeedValue);
            }
        }
    }
}