// Copyright (c) RaaLabs. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;

namespace RaaLabs.Edge.Connectors.NMEA.SentenceFormats
{
    /// <summary> 
    /// Represents the format of vender specific NMEA sentences for Veinland DCU32
    /// </summary>
    public class TEMPAI : ISentenceFormat
    {
        /// <inheritdoc/>
        public string Identitifer => "TEMPAI";
        readonly Parser parser = new Parser();

        /// <inheritdoc/>
        public IEnumerable<TagWithData> Parse(string[] values)
        {
            var flowTempMainEngineIn = values[1];
            var flowTempAuxEngineIn = values[2];
            var flowTempBoilerIn = values[3];


            if (parser.ValidSentenceValue(flowTempMainEngineIn)) yield return new TagWithData("FlowTempMainEngineIn",  parser.StringToDouble(flowTempMainEngineIn));
            if (parser.ValidSentenceValue(flowTempAuxEngineIn)) yield return new TagWithData("FlowTempAuxEngineIn",  parser.StringToDouble(flowTempAuxEngineIn));
            if (parser.ValidSentenceValue(flowTempBoilerIn)) yield return new TagWithData("FlowTempBoilerIn",  parser.StringToDouble(flowTempBoilerIn));

        }
      

    }
}