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

        /// <inheritdoc/>
        public IEnumerable<TagWithData> Parse(string[] values)
        {
            var windAngle = values[0];
            var windSpeed = values[2];

            var windAngleName = "WindAngleTrue";
            var windUnit = "WindSpeedTrue";
            if (values[1] == "R")
            {
                windAngleName = "WindAngleRelative";
                windUnit = "WindSpeedRelative";
            }


            if (ValidSentence(windAngle)) yield return new TagWithData(windAngleName, float.Parse(windAngle, CultureInfo.InvariantCulture.NumberFormat));
            if (ValidSentence(windSpeed))
            {
                var windSpeedValue = float.Parse(windSpeed, CultureInfo.InvariantCulture.NumberFormat);
                if (values[3] == "K") windSpeedValue = windSpeedValue * 1000 / 3600;
                if (values[3] == "N") windSpeedValue = windSpeedValue * 1852 / 3600;
                yield return new TagWithData(windUnit, windSpeedValue);
            }
        }
        private bool ValidSentence(string value)
        {
            return !string.IsNullOrEmpty(value);
        }
    }
}