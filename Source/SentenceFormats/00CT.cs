// Copyright (c) RaaLabs. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;

namespace RaaLabs.Edge.Connectors.NMEA.SentenceFormats
{
    /// <summary> 
    /// Represents the format of vender specific NMEA sentences for Veinland DCU32
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
            var flowAuxEngineIn = values[3];
            var flowBoilerIn = values[5];

            if (parser.ValidSentenceValue(flowMainEngineIn))
            {
                var parsedFlowMainEngineIn = int.Parse(flowMainEngineIn, System.Globalization.NumberStyles.HexNumber);
                yield return new TagWithData("FlowMainEngineIn",  (float) parsedFlowMainEngineIn);
            } 
            if (parser.ValidSentenceValue(flowAuxEngineIn)) 
            {
                var parsedFlowAuxEngineIn = int.Parse(flowAuxEngineIn, System.Globalization.NumberStyles.HexNumber);
                yield return new TagWithData("FlowAuxEngineIn",  (float) parsedFlowAuxEngineIn);
            }

            if (parser.ValidSentenceValue(flowBoilerIn))
            {
                var parsedFlowBoilerIn = int.Parse(flowBoilerIn, System.Globalization.NumberStyles.HexNumber);
                yield return new TagWithData("FlowBoilerIn",  (float) parsedFlowBoilerIn);
            } 

        }
      

    }
}