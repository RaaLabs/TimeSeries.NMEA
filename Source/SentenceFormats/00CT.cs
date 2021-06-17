// Copyright (c) RaaLabs. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;

namespace RaaLabs.Edge.Connectors.NMEA.SentenceFormats
{
    /// <summary> 
    /// Represents the format of "Recommended Minimum Navigation Information"
    /// </summary>
    public class _00CT : ISentenceFormat
    {
        /// <inheritdoc/>
        public string Identitifer => "00CT";
        readonly Parser parser = new Parser();

        /// <inheritdoc/>
        public IEnumerable<TagWithData> Parse(string[] values)
        {
            var flowMainEngineIn = values[1];
            var flowAuxEngineIn = values[2];
            var flowBoilerIn = values[3];


            if (parser.ValidSentenceValue(flowMainEngineIn)) yield return new TagWithData("FlowMainEngineIn",  parser.StringToDouble(flowMainEngineIn));
            if (parser.ValidSentenceValue(flowAuxEngineIn)) yield return new TagWithData("FlowAuxEngineIn",  parser.StringToDouble(flowAuxEngineIn));
            if (parser.ValidSentenceValue(flowBoilerIn)) yield return new TagWithData("FlowBoilerIn",  parser.StringToDouble(flowBoilerIn));

        }
      

    }
}