// Copyright (c) RaaLabs. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Globalization;

namespace RaaLabs.Edge.Connectors.NMEA.SentenceFormats
{
    /// <summary>
    /// Represents the format of "Rate Of Turn"
    /// </summary>
    public class ROT : ISentenceFormat
    {

        /// <inheritdoc/>
        public string Identitifer => "ROT";

        /// <inheritdoc/>
        public IEnumerable<TagWithData> Parse(string[] values)
        {
            var rateOfTurn = values[0];
            if (ValidSentence(rateOfTurn)) yield return new TagWithData("RateOfTurn", float.Parse(rateOfTurn, CultureInfo.InvariantCulture.NumberFormat));
        }

        private bool ValidSentence(string value)
        {
            return !string.IsNullOrEmpty(value);
        }
    }
}