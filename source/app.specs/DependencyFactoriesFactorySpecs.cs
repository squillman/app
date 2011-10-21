using Machine.Specifications;
using app.infrastructure.containers;
using app.infrastructure.containers.simple;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;

namespace app.specs
{
    [Subject(typeof(DependencyFactoriesFactory))]
    public class DependencyFactoriesFactorySpecs
    {
        public abstract class concern : Observes<ICreateDependencyFactories,
                                            DependencyFactoriesFactory>
        {
        }

        public class when_creating_a_dependency_factory : concern
        {
            public class for_a_specific_instance : when_creating_a_dependency_factory
            {
                Establish c = () =>
                {
                    the_instance = new object();
                };

                Because b = () =>
                    result = sut.create_for_instance(the_instance);

                It should_return_a_correctly_initialized_simple_dependency_factory = () =>
                {
                    var item = result.ShouldBeAn<SimpleDependencyFactory>();
                    item.creation_delegate().ShouldEqual(the_instance);
                };

                static object result;
                static object the_instance;
            }

            public class for_an_automatically_created_instance : when_creating_a_dependency_factory
            {
                Establish c = () =>
                {
                    the_container = depends.on<IFetchDependencies>();
                    the_constructor_picker= depends.on<IChooseTheConstructorForAType>();
                };

                Because b = () =>
                    result = sut.create_for_automatic_wiring<MyDependency>();


                It should_return_an_automatically_wiring_dependency_factory_correctly_initialized= () =>
                {
                    var item = result.ShouldBeAn<AutomaticallyWiringDependencyFactory>();
                    item.container.ShouldEqual(the_container);
                    item.dependency_type.ShouldEqual(typeof(MyDependency));
                    item.constructor_picker.ShouldEqual(the_constructor_picker);
                };

                static object result;
                static object the_instance;
                static IFetchDependencies the_container;
                static IChooseTheConstructorForAType the_constructor_picker;
            }

            public class MyDependency
            {
            }
        }
    }
}