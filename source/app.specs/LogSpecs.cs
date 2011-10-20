using Machine.Specifications;
using app.infrastructure.containers;
using app.infrastructure.logging;
using developwithpassion.specifications.rhinomocks;
using developwithpassion.specifications.extensions;

namespace app.specs
{
    [Subject(typeof(Log))]
    public class LogSpecs
    {
        public abstract class concern : Observes
        {

        }

        public class when_a_logger_is_requested : concern
        {
            Establish c = () =>
            {
                the_logger = fake.an<ILogInformation>();
                log_factory = fake.an<ICreateLoggers>();

                the_container = fake.an<IFetchDependencies>();
                ContainerFacadeResolver resolver = () => the_container;
                spec.change(() => Container.facade_resolver).to(resolver);

                the_container.setup(x => x.an<ICreateLoggers>()).Return(log_factory);
                log_factory.setup(x => x.create_logger_bound_to(typeof(when_a_logger_is_requested))).Return(the_logger);
            };

            Because b = () =>
                result = Log.an;

            It should_return_a_logger_created_by_the_container_resolved_log_factory = () =>
                result.ShouldEqual(the_logger);


            static ILogInformation result;
            static ILogInformation the_logger;
            static ICreateLoggers log_factory;
            static IFetchDependencies the_container;
        }
    }
}
