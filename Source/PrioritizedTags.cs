// Copyright (c) RaaLabs. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Collections.ObjectModel;
using RaaLabs.Edge.Modules.Configuration;

namespace RaaLabs.Edge.Connectors.NMEA
{
    /// <summary>
    /// A map of prioritized talkers for all tags
    /// </summary>

    [Name("prioritized.json")]
    public class PrioritizedTags : ReadOnlyDictionary<string, SourcePriority>, IConfiguration
    {
        /// <summary>
        /// 
        /// </summary>
        public PrioritizedTags(IDictionary<string, SourcePriority> priorities) : base(priorities) {}
    }
    /// <summary>
    /// 
    /// </summary>
    public struct SourcePriority
    {
        /// <summary>
        /// Initializes a new instance of <see cref="PrioritizedTags"/>
        /// </summary>
        /// <param name="priority"> A list of all talkers for a tag, in prioritized order </param>
        /// <param name="threshold"> The time before a tag measurement becomes stale </param>
        public SourcePriority(List<string> priority, long threshold)
        {

            Priority = priority;
            Threshold = threshold;
        }

        /// <summary>
        /// A list of all talkers for a tag, in prioritized order
        /// </summary>
        public List<string> Priority { get; set; }

        /// <summary>
        /// The time before a tag measurement becomes stale
        /// </summary>
        public long Threshold { get; set; }
    }

}
