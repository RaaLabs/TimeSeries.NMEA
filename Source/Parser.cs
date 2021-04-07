// Copyright (c) RaaLabs. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Globalization;

namespace RaaLabs.Edge.Connectors.NMEA
{
    /// <summary>
    /// Helper class for parsing
    /// </summary>
    public class Parser
    {
  
        /// <summary>
        /// 
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <param name="cardinalDirectionX"></param>
        /// <param name="cardinalDirectionY"></param>
        /// <returns></returns>

        public IEnumerable<TagWithData> ParsePosition(string latitude, string longitude, string cardinalDirectionX, string cardinalDirectionY)
        {

            if (ValidSentenceValue(latitude) && ValidSentenceValue(cardinalDirectionY))
            {
                var latitudeDeg = ConvertToDegree(latitude);
                if (cardinalDirectionY == "S") latitudeDeg = -latitudeDeg;
                yield return new TagWithData("Latitude", latitudeDeg);

            }
            if (ValidSentenceValue(longitude) && ValidSentenceValue(cardinalDirectionX))
            {
                var longitudeDeg = ConvertToDegree(longitude);
                if (cardinalDirectionX == "W") longitudeDeg = -longitudeDeg;
                yield return new TagWithData("Longitude", longitudeDeg);
            }

            if (ValidSentenceValue(latitude) && ValidSentenceValue(cardinalDirectionY) && ValidSentenceValue(longitude) && ValidSentenceValue(cardinalDirectionX))
            {
                var latitudeDeg = ConvertToDegree(latitude);
                var longitudeDeg = ConvertToDegree(longitude);

                if (cardinalDirectionY == "S") latitudeDeg = -latitudeDeg;
                if (cardinalDirectionX == "W") longitudeDeg = -longitudeDeg;
                yield return new TagWithData("Position", new Coordinate(latitudeDeg, longitudeDeg));
            }
        }

        /// <summary>
        /// Wrapper function for float.Parse
        /// </summary>
        /// <param name="value">string value from NMEA tag</param>
        /// <returns>
        /// float value
        /// </returns>
        public float StringToDouble(string value)
        {
            return float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
        }
        private float ConvertToDegree(string value)
        {
            var length = value.Split(".")[0].Length;
            var _degree = value.Substring(0, length - 2);
            var _decimal = value.Substring(length - 2);
            var result = StringToDouble(_degree) + StringToDouble(_decimal) / 60;

            return result;
        }

        /// <summary>
        /// Check if value is NullOrEmpty. Returns true if can be parsed.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool ValidSentenceValue(string value)
        {
            return !string.IsNullOrEmpty(value);
        }


    }
}