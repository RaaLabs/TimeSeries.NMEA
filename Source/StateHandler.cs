// Copyright (c) RaaLabs. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Linq;
using RaaLabs.Edge.Modules.EventHandling;
using Serilog;

namespace RaaLabs.Edge.Connectors.NMEA
{
    /// <summary>
    /// StateHandler holds state for NMEA signals and emits Events.
    /// </summary>
    public class StateHandler : IConsumeEvent<Events.EventParsed>, IProduceEvent<Events.NMEADatapointOutput>
    {
        /// <inheritdoc/>
        public event EventEmitter<Events.NMEADatapointOutput> SendDatapoint;
        private readonly Dictionary<string, Measurement> _state = new Dictionary<string, Measurement>();
        private readonly Dictionary<string, int> _prioritiesForFullTags;
        private readonly Dictionary<string, long> _timeoutsForTags;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="StateHandler"/>
        /// </summary>
        /// <param name="prioritized"></param>
        /// <param name="logger"><see cref="ILogger"/> for logging</param>
        public StateHandler(PrioritizedTags prioritized, ILogger logger)
        {
            _logger = logger;

            IEnumerable<(string, int)> talkerPrioritiesForTag(string tag, SourcePriority talkerPriorities) =>
                talkerPriorities.Priority.Select((talkerPriority, index) => ($"{tag}.{talkerPriority}", index));

            _prioritiesForFullTags = prioritized.SelectMany(tag => talkerPrioritiesForTag(tag.Key, tag.Value)).ToDictionary(_ => _.Item1, _ => _.Item2);
            _timeoutsForTags = prioritized.ToDictionary(tag => tag.Key, tag => tag.Value.Threshold);
        }


        /// <summary>
        /// Listens to EventParsed events and emmits NMEADatapointOutput
        /// Updates state if measuremens with higher priority are received
        /// </summary>
        public void Handle(Events.EventParsed @event)
        {
            var tagWithTalker = $"{@event.tag}.{@event.talker}";
            var tag = @event.tag;

            var measurement = new Measurement
            {
                tag = @event.tag,
                value = @event.value,
                timestamp = @event.timestamp,
                source = tagWithTalker
            };

            var timeout = _timeoutsForTags.GetValueOrDefault(tag);
            var hasCurrentState = _state.TryGetValue(tag, out Measurement currentState);
            
            if (currentState == null) hasCurrentState = false;

            var currentTimestamp = currentState?.timestamp ?? -1;
            var currentPriority = hasCurrentState ? _prioritiesForFullTags.GetValueOrDefault(currentState.source, int.MaxValue) : int.MaxValue;
            var thisPriority = _prioritiesForFullTags.GetValueOrDefault(tagWithTalker, int.MaxValue);
            var hasHigherPriority = thisPriority <= currentPriority;
            var hasPriorityChanged = thisPriority != currentPriority;
            var currentStateStale = (@event.timestamp - currentTimestamp) > timeout;
            var shouldSetState = !hasCurrentState || hasHigherPriority || currentStateStale;

            var hasOverlappingTalkersWithoutPriority = thisPriority == int.MaxValue && hasCurrentState && !tagWithTalker.Equals(currentState.source);

            if (hasOverlappingTalkersWithoutPriority)
            {
                _logger.Information($"{tagWithTalker} is not configured with a priority, despite {currentState.source} also being a source for this tag.");
            }

            if (hasPriorityChanged && shouldSetState && !string.IsNullOrEmpty(currentState?.source))
            {
                var currentTalker = currentState.source.Split(".").LastOrDefault();
                _logger.Warning($"{@event.tag} changed priority from {currentTalker} to {@event.talker}");
            }

            if (shouldSetState)
            {
                _state[tag] = measurement;
                var output = new Events.NMEADatapointOutput
                {
                    source = "NMEA",
                    tag = @event.tag,
                    timestamp = @event.timestamp,
                    value = @event.value
                };
                SendDatapoint(output);
            }
        }

        /// <summary>
        /// A state data point at a certain time
        /// </summary>
        public class Measurement
        {
            /// <summary>
            /// The measurement name
            /// </summary>
            public string tag { get; set; }

            /// <summary>
            /// The value for the measurement
            /// </summary>
            public dynamic value { get; set; }

            /// <summary>
            /// The timestamp for the measurement
            /// </summary>
            public long timestamp { get; set; }

            /// <summary>
            /// The source for the measurement, e.g. "Latitude.GPRPC"
            /// </summary>
            public string source { get; set; }
        }
    }
}
