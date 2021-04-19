// Copyright (c) RaaLabs. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;

namespace RaaLabs.Edge.Connectors.NMEA.SentenceFormats
{
    /// <summary>
    /// Represents the format of "Rate Of Turn"
    /// </summary>
    public class ROT : ISentenceFormat
    {

        /// <inheritdoc/>
        public string Identitifer => "ROT";
        readonly Parser parser = new Parser();

        /// <inheritdoc/>
        public IEnumerable<TagWithData> Parse(string[] values)
        {
            var rateOfTurn = values[0];
            if (parser.ValidSentenceValue(rateOfTurn)) yield return new TagWithData("RateOfTurn", parser.StringToDouble(rateOfTurn));
        }
    }
}