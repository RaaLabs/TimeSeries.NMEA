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

        /// <inheritdoc/>
        public IEnumerable<TagWithData> Parse(string[] values)
        {
            var latitude = values[1];
            var longitude = values[3];
            var cardinalDirectionY = values[2];
            var cardinalDirectionX = values[4];
            var gpsSatellites = values[6];
            var hdop = values[7];

            if (ValidSentence(gpsSatellites)) yield return new TagWithData("GPSsatellites", float.Parse(gpsSatellites, CultureInfo.InvariantCulture.NumberFormat));
            if (ValidSentence(hdop)) yield return new TagWithData("HDOP", float.Parse(hdop, CultureInfo.InvariantCulture.NumberFormat));

            if (ValidSentence(latitude) && ValidSentence(cardinalDirectionY))
            {
                var latitudeDeg = ConvertToDegree(latitude);
                if (cardinalDirectionY == "S") latitudeDeg = -latitudeDeg;
                yield return new TagWithData("Latitude", latitudeDeg);

            }
            if (ValidSentence(longitude) && ValidSentence(cardinalDirectionX))
            {
                var longitudeDeg = ConvertToDegree(longitude);
                if (cardinalDirectionX == "W") longitudeDeg = -longitudeDeg;
                yield return new TagWithData("Longitude", longitudeDeg);
            }

            if (ValidSentence(latitude) && ValidSentence(cardinalDirectionY) && ValidSentence(longitude) && ValidSentence(cardinalDirectionX))
            {
                var latitudeDeg = ConvertToDegree(latitude);
                var longitudeDeg = ConvertToDegree(longitude);

                if (cardinalDirectionY == "S") latitudeDeg = -latitudeDeg;
                if (cardinalDirectionX == "W") longitudeDeg = -longitudeDeg;

                yield return new TagWithData("Position", new Coordinate(latitudeDeg, longitudeDeg));

            }
        }

        private float ConvertToDegree(string value)
        {
            var length = value.Split(".")[0].Length;
            var _degree = value.Substring(0, length - 2);
            var _decimal = value.Substring(length - 2);
            var result = float.Parse(_degree, CultureInfo.InvariantCulture.NumberFormat) + float.Parse(_decimal, CultureInfo.InvariantCulture.NumberFormat) / 60;

            return result;
        }
        private bool ValidSentence(string value)
        {
            return !string.IsNullOrEmpty(value);
        }

    }
}