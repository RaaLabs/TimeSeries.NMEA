/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Globalization;
using RaaLabs.Edge.Connectors.NMEA.events;

namespace RaaLabs.Edge.Connectors.NMEA.SentenceFormats
{
    /// <summary> 
    /// Represents the format of "Track made good and Ground speed"
    /// </summary>
    public class VTG : ISentenceFormat
    {

        /// <inheritdoc/>
        public string Identitifer => "VTG";

        /// <inheritdoc/>

        public IEnumerable<TagWithData> Parse(string[] values)
        {
            var courseOverGroundTrue = values[0];
            var courseOverGroundMagnetic = values[2];
            var speedOverGround = values[4];

            if (ValidSentence(courseOverGroundTrue)) yield return new TagWithData("CourseOverGroundTrue", float.Parse(courseOverGroundTrue, CultureInfo.InvariantCulture.NumberFormat));
            if (ValidSentence(courseOverGroundMagnetic)) yield return new TagWithData("CourseOverGroundMagnetic", float.Parse(courseOverGroundMagnetic, CultureInfo.InvariantCulture.NumberFormat));
            if (ValidSentence(speedOverGround)) yield return new TagWithData("SpeedOverGround", (float.Parse(speedOverGround, CultureInfo.InvariantCulture.NumberFormat) * 1852) / 3600);
        }

        private bool ValidSentence(string value)
        {
            return !string.IsNullOrEmpty(value);
        }
    }
}