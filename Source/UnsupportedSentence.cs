// Copyright (c) RaaLabs. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace RaaLabs.Edge.Connectors.NMEA
{
    /// <summary>
    /// Exception that gets thrown if a sentence is not supported
    /// </summary>
    public class UnsupportedSentence : Exception
    {
        /// <summary>
        /// Initializes a new instance of <see cref="UnsupportedSentence"/>
        /// </summary>
        /// <param name="sentence">Unsupported sentence</param>
        /// <param name="talker">Talker that is not supported</param>
        /// <param name="identifier">Identifier that is not supported</param>
        public UnsupportedSentence(string sentence, string talker, string identifier) 
            : base($"Talker '{talker}' with identifier '{identifier}' is not supported in sentence '{sentence}'")
        {

        }
       
    }    
}