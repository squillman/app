using System;
using System.ComponentModel;
using Machine.Specifications;
using app.tasks.startup;
using app.tasks.startup.pipeline;
using app.web.infrastructure;
using developwithpassion.specifications.rhinomocks;
using Container = app.infrastructure.containers.Container;
using developwithpassion.specifications.extensions;

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

        public class integration
        {
            public class when_run:concern
            {
                Because b = () =>
                {
                    Start.by<ConfiguringTheContainer>();
                };

                It should_be_able_to_access_key_services = () =>
                    Container.fetch
            } 
        }
    }

    public class SomeCommand : IPlayAPartInApplicationStartUp
    {
        public void run()
        {
        }
    }
}