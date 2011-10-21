using Machine.Specifications;
using app.infrastructure;
using developwithpassion.specifications.rhinomocks;
using developwithpassion.specifications.extensions;

namespace app.specs
{
    [Subject(typeof(CommandExtensions))]
    public class CommandExtensionsSpecs
    {
        public abstract class concern : Observes
        {

        }

        public class when_chaining_a_command_to_an_existing_command : concern
        {
            Establish c = () =>
            {
                the_first = fake.an<IEncapsulateABehaviour>();
                the_second = fake.an<IEncapsulateABehaviour>();
            };

            Because b = () =>
                result = CommandExtensions.followed_by(the_first, the_second);

            It should_return_a_chained_command_correctly_initialized = () =>
            {
                var command = result.ShouldBeAn<ChainedCommand>();
                command.first.ShouldEqual(the_first);
                command.second.ShouldEqual(the_second);

            };

            static IEncapsulateABehaviour result;
            static IEncapsulateABehaviour the_first;
            static IEncapsulateABehaviour the_second;
        }
    }
}
