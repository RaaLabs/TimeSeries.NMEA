// Copyright (c) RaaLabs. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System;

namespace RaaLabs.Edge.Connectors.NMEA.SentenceFormats
{
    /// <summary>
    /// Represents the format of "Dual Ground/Water Speed (should probably implement only send if status is valid, see documentation)"
    /// </summary>
    public class VBW : ISentenceFormat
    {
        /// <inheritdoc/>
        public string Identitifer => "VBW";
        readonly Parser parser = new Parser();


        /// <inheritdoc/>
        public IEnumerable<TagWithData> Parse(string[] values)
        {
            var longitudinalSpeedThroughWater = values[0];
            var transverseSpeedThroughWater = values[1];
            var longitudinalSpeedOverGround = values[3];
            var transverseSpeedOverGround = values[4];

            if (parser.ValidSentenceValue(longitudinalSpeedThroughWater)) yield return new TagWithData("LongitudinalSpeedThroughWater", parser.StringToDouble(longitudinalSpeedThroughWater) * 1852 / 3600);
            if (parser.ValidSentenceValue(transverseSpeedThroughWater)) yield return new TagWithData("TransverseSpeedThroughWater", parser.StringToDouble(transverseSpeedThroughWater) * 1852 / 3600);
            if (parser.ValidSentenceValue(longitudinalSpeedOverGround)) yield return new TagWithData("LongitudinalSpeedOverGround", parser.StringToDouble(longitudinalSpeedOverGround) * 1852 / 3600);
            if (parser.ValidSentenceValue(transverseSpeedOverGround)) yield return new TagWithData("TransverseSpeedOverGround", parser.StringToDouble(transverseSpeedOverGround) * 1852 / 3600);
            if (parser.ValidSentenceValue(longitudinalSpeedThroughWater)) 
            {
                var transverse = parser.ValidSentenceValue(transverseSpeedThroughWater) ? parser.StringToDouble(transverseSpeedThroughWater) : 0.0f;
                var longitudinal = parser.StringToDouble(longitudinalSpeedThroughWater);
                var speedThroughWater = (float) Math.Sqrt(Math.Pow(longitudinal, 2) + Math.Pow(transverse, 2));
                yield return new TagWithData("SpeedThroughWater", speedThroughWater * 1852 / 3600);
            }
        }
    }
}
