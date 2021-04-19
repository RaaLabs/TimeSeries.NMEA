// Copyright (c) RaaLabs. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Globalization;

namespace RaaLabs.Edge.Connectors.NMEA.SentenceFormats
{
    /// <summary>
    /// Represents the format of "Heading - True"
    /// </summary>
    public class HDT : ISentenceFormat
    {

        /// <inheritdoc/>
        public string Identitifer => "HDT";
        readonly Parser parser = new Parser();

        /// <inheritdoc/>
        public IEnumerable<TagWithData> Parse(string[] values)
        {
            var headingTrue = values[0];
            if (parser.ValidSentenceValue(headingTrue)) yield return new TagWithData("HeadingTrue", parser.StringToDouble(headingTrue));
        }
    }
}