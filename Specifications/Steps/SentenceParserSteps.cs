using BoDi;
using RaaLabs.Edge.Modules.EventHandling;
using RaaLabs.Edge.Connectors.NMEA.Specs.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TechTalk.SpecFlow;
using FluentAssertions;

namespace RaaLabs.Edge.Connectors.NMEA.Specs.Steps
{
    [Binding]
    public sealed class SentenceParserSteps
    {
        private SentenceParser _parser;
        private bool _validSentence; 
        private Exception _exception;
        private readonly IObjectContainer _container;


        public SentenceParserSteps(IObjectContainer container, SentenceParser parser)
        {
            _container = container;
            _parser = parser;
        }

        [When(@"sentence ""(.*)"" is parsed")]
        public void WhenSentenceIsParsed(string sentence)
        {
            try
            {
                _validSentence = _parser.IsValidSentence(sentence);
                _parser.Parse(sentence);
            }
            catch (Exception ex)
            {
                _exception = ex;
            }
        }

        [Then(@"the exception ""(.*)"" is thrown with message ""(.*)""")]
        public void ThenTheExeptionIsThrownWithMessage(string type, string message)
        {
            Console.WriteLine(_exception.GetType().Name);
            Console.WriteLine(_exception.Message);
            _exception.Message.Should().Be(message);
            _exception.GetType().Name.Should().Be(type);
        }

        [Then(@"the sentence should be valid")]
        public void ThenTheSentenceShould()
        {
            _validSentence.Should().Be(true);

        }

    }
}
