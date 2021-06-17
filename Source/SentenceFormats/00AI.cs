// Copyright (c) RaaLabs. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;

namespace RaaLabs.Edge.Connectors.NMEA.SentenceFormats
{
    /// <summary> 
    /// Represents the format of vender specific NMEA sentences for Veinland DCU32
    /// </summary>
    public class _00AI : ISentenceFormat
    {
        /// <inheritdoc/>
        public string Identitifer => "00AI";
        readonly Parser parser = new Parser();

        /// <inheritdoc/>
        public IEnumerable<TagWithData> Parse(string[] values)
        {
            var shaftThrust  = values[1];
            var shaftRpm = values[2];
            var shaftTorque = values[3];
            var shaftPower = values[4];


            if (parser.ValidSentenceValue(shaftThrust)) yield return new TagWithData("ShaftThrust",  parser.StringToDouble(shaftThrust));
            if (parser.ValidSentenceValue(shaftRpm)) yield return new TagWithData("ShaftRpm",  parser.StringToDouble(shaftRpm));
            if (parser.ValidSentenceValue(shaftTorque)) yield return new TagWithData("ShaftTorque",  parser.StringToDouble(shaftTorque));
            if (parser.ValidSentenceValue(shaftPower)) yield return new TagWithData("ShaftPower",  parser.StringToDouble(shaftPower));

        }
      

    }
}