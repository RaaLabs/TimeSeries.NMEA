// Copyright (c) RaaLabs. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Linq;
using RaaLabs.Edge.Modules.EventHandling;
using Serilog;

namespace RaaLabs.Edge.Connectors.NMEA
{
    /// <summary>
    /// Layer for checking if events can be parsed. 
    /// Events that can be parsed are emited as EventParsed
    /// </summary>
    public class NMEALineHandler : IConsumeEvent<Events.NMEASentenceReceived>, IProduceEvent<Events.EventParsed>
    {
        /// <summary>
        /// Initializes a new instance <see cref="Events.EventParsed"/>
        /// </summary>
        public event EventEmitter<Events.EventParsed> EventParsed;
        private readonly ILogger _logger;
        private readonly SentenceParser _parser;

        /// <summary>
        /// Initializes a new instance <see cref="NMEALineHandler"/>
        /// </summary>
        /// <param name="parser"> <see cref="SentenceParser">formats</see></param>
        /// <param name="logger"> <see cref="ILogger">formats</see> </param>

        public NMEALineHandler(SentenceParser parser, ILogger logger)
        {
            _logger = logger;
            _parser = parser;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Handle(Events.NMEASentenceReceived @event)
        {
            try
            {
                if (_parser.CanParse(@event.Sentence))
                {
                    var identifier = _parser.GetIdentifierFor(@event.Sentence);

                    var tags = _parser.Parse(@event.Sentence).ToList();
                    var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                    tags.ForEach(tag =>
                    {
                        var output = new Events.EventParsed
                        {
                            Talker = identifier,
                            Tag = tag.Tag,
                            Timestamp = timestamp,
                            Value = tag.Data
                        };
                        EventParsed(output);
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Trouble parsing {event}", @event);
            }
        }
    }
}