using Machine.Specifications;
using app.infrastructure.containers.simple;
using app.tasks.startup;
using developwithpassion.specifications.rhinomocks;
using developwithpassion.specifications.extensions;

namespace app.specs
{
    [Subject(typeof(ContainerRegistrationFacility))]
    public class ContainerRegistrationFacilitySpecs
    {
        public abstract class concern : Observes<IRegisterComponentsIntoTheContainer,
                                            ContainerRegistrationFacility>
        {
        }

        public class when_an_item_is_registered : concern
        {
            public class with_its_contract_and_implementation : when_an_item_is_registered
            {
                Establish c = () =>
                {
                    factories = depends.on<ICreateDependencyFactories>();
                    the_factory = fake.an<ICreateASingleDependency>();


                    factories.setup(x => x.create_for_automatic_wiring<TheDependency>())
                        .Return(the_factory);
                };

                Because b = () =>
                    sut.add_factory<IAmADependency, TheDependency>();

                It should_store_the_created_factory_using_the_correct_key = () =>
                    sut[typeof(IAmADependency)].ShouldEqual(the_factory);

                static ICreateASingleDependency the_factory;
                static ICreateDependencyFactories factories;
            }

            public class with_just_the_implementation : when_an_item_is_registered
            {
                Establish c = () =>
                {
                    factories = depends.on<ICreateDependencyFactories>();
                    the_factory = fake.an<ICreateASingleDependency>();


                    factories.setup(x => x.create_for_automatic_wiring<TheDependency>())
                        .Return(the_factory);
                };

                Because b = () =>
                    sut.add_factory<TheDependency>();

                It should_store_the_created_factory_using_the_correct_key = () =>
                    sut[typeof(TheDependency)].ShouldEqual(the_factory);

                static ICreateASingleDependency the_factory;
                static ICreateDependencyFactories factories;
            }

            public class with_the_instance : when_an_item_is_registered
            {
                Establish c = () =>
                {
                    factories = depends.on<ICreateDependencyFactories>();
                    the_factory = fake.an<ICreateASingleDependency>();
                    the_instance = new TheDependency();

                    factories.setup(x => x.create_for_instance(the_instance))
                        .Return(the_factory);
                };

                Because b = () =>
                    sut.add_instance(the_instance);

                It should_store_the_created_factory_using_the_correct_key = () =>
                    sut[typeof(TheDependency)].ShouldEqual(the_factory);

                static ICreateASingleDependency the_factory;
                static ICreateDependencyFactories factories;
                static TheDependency the_instance;
            }
        }

        public class TheDependency : IAmADependency
        {
        }

        public interface IAmADependency
        {
        }
    }
}