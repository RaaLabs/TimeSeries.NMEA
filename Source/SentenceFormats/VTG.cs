// Copyright (c) RaaLabs. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Globalization;

namespace RaaLabs.Edge.Connectors.NMEA.SentenceFormats
{
    /// <summary> 
    /// Represents the format of "Track made good and Ground speed"
    /// </summary>
    public class VTG : ISentenceFormat
    {

        /// <inheritdoc/>
        public string Identitifer => "VTG";
        readonly Parser parser = new Parser();

        /// <inheritdoc/>
        public IEnumerable<TagWithData> Parse(string[] values)
        {
            var courseOverGroundTrue = values[0];
            var courseOverGroundMagnetic = values[2];
            var speedOverGround = values[4];

            if (parser.ValidSentenceValue(courseOverGroundTrue)) yield return new TagWithData("CourseOverGroundTrue", parser.StringToDouble(courseOverGroundTrue));
            if (parser.ValidSentenceValue(courseOverGroundMagnetic)) yield return new TagWithData("CourseOverGroundMagnetic", parser.StringToDouble(courseOverGroundMagnetic));
            if (parser.ValidSentenceValue(speedOverGround)) yield return new TagWithData("SpeedOverGround", parser.KnotsToMps(speedOverGround));
        }
    }
}