// Copyright (c) RaaLabs. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;

namespace RaaLabs.Edge.Connectors.NMEA.SentenceFormats
{
    /// <summary>
    /// Represents the format of "Depth of Water"
    /// </summary>
    public class DPT : ISentenceFormat
    {

        /// <inheritdoc/>
        public string Identitifer => "DPT";
        readonly Parser parser = new Parser();

        /// <inheritdoc/>
        public IEnumerable<TagWithData> Parse(string[] values)
        {
            var name = "WaterDepth";
            var waterDepthRelativeToTransducer = values[0];
            var offsetFromTransducer = values[1];

            if (parser.ValidSentenceValue(waterDepthRelativeToTransducer))
            {
                if (offsetFromTransducer.Contains("-"))
                {
                    name = "DepthBelowKeel";
                }

                if (parser.ValidSentenceValue(waterDepthRelativeToTransducer) && parser.ValidSentenceValue(offsetFromTransducer))
                {
                    var waterDepth = parser.StringToDouble(waterDepthRelativeToTransducer);
                    var offset = parser.StringToDouble(offsetFromTransducer);
                    yield return new TagWithData(name, waterDepth + offset);
                }
                else
                {
                    throw new InvalidSentenceException($"DPT: Unable to parse '{waterDepthRelativeToTransducer}' and/or '{offsetFromTransducer}'");
                }
            }
        }

    }
}