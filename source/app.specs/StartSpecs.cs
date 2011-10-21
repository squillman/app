using System;
using Machine.Specifications;
using app.infrastructure.containers;
using app.tasks.startup;
using app.tasks.startup.pipeline;
using developwithpassion.specifications.rhinomocks;

namespace app.specs
{
    [Subject(typeof(Start))]
    public class StartSpecs
    {
        public abstract class concern : Observes
        {
        }

        public class when_specifying_the_first_element_in_a_startup_chain : concern
        {
            Establish c = () =>
            {
                the_startup_builder = fake.an<IComposeStartupChains>();

                Func<Type, IComposeStartupChains> factory = x =>
                {
                    x.ShouldEqual(typeof(SomeCommand));

                    return the_startup_builder;
                };

                spec.change(() => Start.builder_factory).to(factory);
            };

            Because b = () =>
                result = Start.by<SomeCommand>();

            It should_return_the_startup_pipeline_builder_created_using_the_type_of_the_first_element = () =>
                result.ShouldEqual(the_startup_builder);

            static IComposeStartupChains result;
            static IComposeStartupChains the_startup_builder;
        }

    }

    public class SomeCommand : IPlayAPartInApplicationStartUp
    {
        public void run()
        {
        }
    }
}