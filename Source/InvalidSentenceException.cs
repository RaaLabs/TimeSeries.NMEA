// Copyright (c) RaaLabs. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace RaaLabs.Edge.Connectors.NMEA
{
    /// <summary>
    /// Exception that gets thrown if a sentence is in the invalid format
    /// </summary>
    public class InvalidSentenceException : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="InvalidSentenceException"/>
        /// </summary>
        /// <param name="sentence">The invalid sentence</param>
        public InvalidSentenceException(string sentence) : base($"Sentence '{sentence}' is invalid. Please refer to the standard for NMEA.")
        {

        }
    }   
}