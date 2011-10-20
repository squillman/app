using System;
using Machine.Specifications;
using app.infrastructure.containers;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;

namespace app.specs
{
    [Subject(typeof(ContainerFacade))]
    public class ContainerFacadeSpecs
    {
        public abstract class concern : Observes<IFetchDependencies, ContainerFacade>
        {
        }

        public class when_fetching_a_dependency : concern
        {
            public class and_the_dependency_could_be_created_successfully
            {
                Establish c = () =>
                {
                    the_dependency = new ADependency();

                    depency_factories = depends.on<IFindFactoriesForDependencies>();
                    dependency_factory = fake.an<ICreateASingleDependency>();

                    depency_factories.setup(x => x.get_the_factory_that_can_create(typeof(ADependency))).Return(
                        dependency_factory);
                    dependency_factory.setup(x => x.create()).Return(the_dependency);
                };

                Because b = () =>
                    result = sut.an<ADependency>();

                It should_return_the_dependency_created_using_the_factory_for_the_dependency = () =>
                    result.ShouldEqual(the_dependency);
            }

            static ADependency result;
            static ADependency the_dependency;
            static ICreateASingleDependency dependency_factory;
            static IFindFactoriesForDependencies depency_factories;

            public class and_the_underlying_factory_throws_an_exception_while_trying_to_create_the_dependency :
                when_fetching_a_dependency
            {
                Establish c = () =>
                {
                    the_dependency = new ADependency();

                    depency_factories = depends.on<IFindFactoriesForDependencies>();
                    dependency_factory = fake.an<ICreateASingleDependency>();

                    depency_factories.setup(x => x.get_the_factory_that_can_create(typeof(ADependency))).Return(
                        dependency_factory);

                    the_original_exception = new Exception();
                    dependency_factory.setup(x => x.create()).Throw(the_original_exception);
                };

                Because b = () =>
                    spec.catch_exception(() => sut.an<ADependency>());

                It should_throw_a_dependency_creation_exception_with_the_necessary_details = () =>
                {
                    var exception = spec.exception_thrown.ShouldBeAn<DependencyCreationException>();
                    exception.InnerException.ShouldEqual(the_original_exception);
                    exception.type_that_could_not_be_created.ShouldEqual(typeof(ADependency));
                };

                static Exception the_original_exception;
            }
        }
    }

    public class ADependency
    {
    }
}