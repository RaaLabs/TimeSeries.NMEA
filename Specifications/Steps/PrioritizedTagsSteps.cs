using BoDi;
using System.Linq;
using TechTalk.SpecFlow;
using System.Globalization;

namespace RaaLabs.Edge.Connectors.NMEA.Specs.Steps
{
    [Binding]
    public sealed class PrioritizedTagsSteps
    { 
        private readonly IObjectContainer _container;

        public PrioritizedTagsSteps(IObjectContainer container)
        {
            _container = container;
        }

        [Given(@"the prioritized tags")]
        public void GivenThePrioritizedTags(Table table)
        {
            var prioritizedTagsEnum = table.Rows.Select(row => (Tag: row["Tag"], new SourcePriority(row["Priority"].Split(',').ToList(), threshold: long.Parse(row["Threshold"], CultureInfo.InvariantCulture.NumberFormat))));
            var prioritizedTags = prioritizedTagsEnum.ToDictionary(x => x.Tag, x => x.Item2);
            var identities = new PrioritizedTags(prioritizedTags);
            _container.RegisterInstanceAs<PrioritizedTags>(identities);
        }
    }
}