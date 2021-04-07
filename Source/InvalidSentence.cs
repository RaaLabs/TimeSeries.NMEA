/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;

namespace RaaLabs.Edge.Connectors.NMEA
{
    /// <summary>
    /// Exception that gets thrown if a sentence is in the invalid format
    /// </summary>
    public class InvalidSentence : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="InvalidSentence"/>
        /// </summary>
        /// <param name="sentence">The invalid sentence</param>
        public InvalidSentence(string sentence) : base($"Sentence '{sentence}' is invalid. Please refer to the standard for NMEA.")
        {

        }
    }   
}