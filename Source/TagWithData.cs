// Copyright (c) RaaLabs. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.


namespace RaaLabs.Edge.Connectors.NMEA
{
    /// <summary>
    /// Represents the sensor names and values
    /// </summary>
    public class TagWithData
    {
        /// <summary>
        /// Initializes a new instance of <see cref="TagWithData"/>
        /// </summary>
        /// <param name="tag"><see cref="Tag"/> the data is for</param>
        /// <param name="data"><see cref="Data"/> for the tag</param>
        public TagWithData(string tag, object data)
        {
            Tag = tag;
            Data = data;
        }

        /// <summary>
        /// Get the name representing the system tag name
        /// </summary>
        public string Tag { get; set; }

         /// <summary>
        /// Get the value representing the data
        /// </summary>
        public object Data { get; set;}

    }
}
