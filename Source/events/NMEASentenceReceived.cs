using RaaLabs.Edge.Modules.EventHandling;

namespace RaaLabs.Edge.Connectors.NMEA.events
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sentence"></param>
        public NMEASentenceReceived(string sentence)
        {
            Sentence = sentence;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sentence"></param>
        public static implicit operator NMEASentenceReceived(string sentence) => new NMEASentenceReceived(sentence);
    }
}
