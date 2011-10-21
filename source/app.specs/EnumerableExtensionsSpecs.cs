using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using app.infrastructure;
using developwithpassion.specifications.rhinomocks;

namespace app.specs
{
    [Subject(typeof(EnumerableExtensions))]
    public class EnumerableExtensionsSpecs
    {
        public abstract class concern : Observes
        {
        }

        public class when_processing_each_element_of_an_iterator_with_a_visitor : concern
        {
            Establish c = () =>
            {
                all_items = Enumerable.Range(1, 100);
            };

            Because b = () =>
                EnumerableExtensions.each(all_items, x => number_of_items_visited++);

            It should_run_the_visitor_against_each_item = () =>
                number_of_items_visited.ShouldEqual(all_items.Count());

            static int number_of_items_visited;
            static IEnumerable<int> all_items;
        }
    }
}