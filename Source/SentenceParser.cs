/*---------------------------------------------------------------------------------------------
 *  Copyright (c) RaaLabs. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Serilog;

namespace RaaLabs.Edge.Connectors.NMEA
{

    /// <summary>
    /// Defines the parser of sentences
    /// </summary>
    public class SentenceParser
    {
        readonly IDictionary<string, ISentenceFormat> _formats;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance <see cref="SentenceParser"/>
        /// </summary>
        /// <param name="formats">All available <see cref="ISentenceFormat">formats</see></param>
        /// <param name="logger"> <see cref="ILogger">formats</see> </param>
        public SentenceParser(IEnumerable<ISentenceFormat> formats, ILogger logger)
        {
            _formats = formats.ToDictionary(_ => $"{_.Identitifer}", _ => _);
            _logger = logger;
        }

        /// <summary>
        /// Check if sentence can be parsed
        /// </summary>
        /// <param name="sentence">Sentence to check</param>
        /// <returns>True if it can be parsed, false if not</returns>
        public bool CanParse(string sentence)
        {
            if (!IsValidSentence(sentence)) return false;
            sentence = sentence.Substring(1);
            var identifier = sentence.Substring(2, 3);
            if (!_formats.ContainsKey(identifier))
            {
                _logger.Information($"Identifier '{identifier}' is not supported, can not parse {sentence}");
            }
            return _formats.ContainsKey(identifier);
        }

        /// <summary>
        /// Get the unique sentence identifier for the sentence
        /// </summary>
        /// <param name="sentence">The sentence to get for</param>
        /// <returns>Unique identifier for the sentence</returns>
        public string GetIdentifierFor(string sentence)
        {
            ThrowIfSentenceIsInvalid(sentence);
            return sentence.Substring(1, 5);
        }

        /// <summary>
        /// Parse a parseable sentence into its target object
        /// </summary>
        /// <param name="sentence">Sentence to parse</param>
        /// <returns>All the results parsed</returns>
        public IEnumerable<TagWithData> Parse(string sentence)
        {
            ThrowIfSentenceIsInvalid(sentence);
            var originalSentence = sentence;

            var formatIdentifier = sentence.Substring(1, 5);
            var talker = formatIdentifier.Substring(0, 2);
            var identifier = formatIdentifier.Substring(2);
            ThrowIfUnsupportedSentence(originalSentence, talker, identifier);

            if (sentence[sentence.Length - 3] == '*')
            {
                var checksum = Byte.Parse(sentence.Substring(sentence.Length - 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture.NumberFormat);
                sentence = sentence.Substring(1, sentence.Length - 4);

                byte calculatedChecksum = 0;
                for (var i = 0; i < sentence.Length; i++) calculatedChecksum ^= (byte)sentence[i];
                ThrowIfSentenceChecksumIsInvalid(sentence, checksum, calculatedChecksum);
            }
            else sentence = sentence.Substring(1);

            var values = sentence.Substring(6).Split(',');
            var result = _formats[identifier].Parse(values);

            return result;
        }

        /// <summary>
        /// Wrapper function for float.Parse
        /// </summary>
        /// <param name="value">string value from NMEA tag</param>
        /// <returns>
        /// float value
        /// </returns>
        public float FloatParse(string value)
        {
            return float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
        }

        bool IsValidSentence(string sentence)
        {
            if (!sentence.StartsWith('$')) return false;
            if (sentence.Length < 6) return false;
            return true;
        }

        void ThrowIfSentenceIsInvalid(string sentence)
        {
            if (!IsValidSentence(sentence)) throw new InvalidSentence(sentence);
        }

        void ThrowIfUnsupportedSentence(string sentence, string talker, string identifier)
        {
            if (!_formats.ContainsKey($"{identifier}")) throw new UnsupportedSentence(sentence, talker, identifier);
        }

        void ThrowIfSentenceChecksumIsInvalid(string sentence, byte actualChecksum, byte expectedChecksum)
        {
            if (expectedChecksum != actualChecksum) throw new InvalidSentenceChecksum(actualChecksum, expectedChecksum, sentence);
        }
    }
}