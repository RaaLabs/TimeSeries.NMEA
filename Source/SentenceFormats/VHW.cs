// Copyright (c) RaaLabs. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Globalization;

namespace RaaLabs.Edge.Connectors.NMEA.SentenceFormats
{
    /// <summary> 
    /// VHW - Water speed and heading
    /// </summary>
    public class VHW : ISentenceFormat
    {

        /// <inheritdoc/>
        public string Identitifer => "VHW";
        readonly Parser parser = new Parser();


        /// <inheritdoc/>
        public IEnumerable<TagWithData> Parse(string[] values)
        {
            var headingTrue = values[0];
            var headingMagnetic = values[2];
            var speedThroughWater = values[4];

            if(parser.ValidSentenceValue(headingTrue)) yield return  new TagWithData("HeadingTrue", parser.StringToDouble(headingTrue));
            if(parser.ValidSentenceValue(headingMagnetic)) yield return  new TagWithData("HeadingMagnetic", parser.StringToDouble(headingMagnetic));
            if(parser.ValidSentenceValue(speedThroughWater)) yield return  new TagWithData("SpeedThroughWater", parser.StringToDouble(speedThroughWater)*1852 / 3600);
        }

    }
}