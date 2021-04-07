/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq;
using RaaLabs.Edge.Modules.EventHandling;
using Serilog;

namespace RaaLabs.Edge.Connectors.NMEA
{
    class NMEALineHandler : IConsumeEvent<events.NMEASentenceReceived>, IProduceEvent<events.EventParsed>
    {
        public event EventEmitter<events.EventParsed> EventParsed;
        private readonly ILogger _logger;
        private readonly SentenceParser _parser;

        public NMEALineHandler(SentenceParser parser, ILogger logger)
        {
            _logger = logger;
            _parser = parser;
        }

        public void Handle(events.NMEASentenceReceived @event)
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
                        var output = new events.EventParsed
                        {
                            talker = identifier,
                            tag = tag.Tag,
                            timestamp = timestamp,
                            value = tag.Data
                        };
                        EventParsed(output);
                    });
                }
            }
            catch (FormatException ex)
            {
                _logger.Error(ex, $"Trouble parsing  {@event}");
            }
        }
    }
}