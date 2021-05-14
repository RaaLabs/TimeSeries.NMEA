// Copyright (c) RaaLabs. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using RaaLabs.Edge.Modules.EventHandling;

namespace RaaLabs.Edge.Connectors.NMEA.Events
{
    /// <summary>
    /// Implements event for NMEASentenceReceived 
    /// </summary>
    public class NMEASentenceReceived : IEvent
    {

        /// <summary>
        /// gets and sets the NMEA sentence. 
        /// </summary>
        public string Sentence { get; set; }

        /// <inheritdoc/>
        public NMEASentenceReceived(string sentence)
        {
            Sentence = sentence;
        }
        
        /// <summary>
        /// NMEA sentence
        /// </summary>
        /// <param name="sentence"></param>
        public static implicit operator NMEASentenceReceived(string sentence) => new NMEASentenceReceived(sentence);
    }
}
