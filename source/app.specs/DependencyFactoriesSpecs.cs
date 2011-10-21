using System;
using System.Collections.Generic;
using Machine.Specifications;
using app.infrastructure.containers.simple;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;

namespace app.specs
{
    [Subject(typeof(DependencyFactories))]
    public class DependencyFactoriesSpecs
    {
        public abstract class concern : Observes<IFindFactoriesForDependencies,
                                            DependencyFactories>
        {
        }

        public class when_finding_a_factory_for_a_dependency : concern
        {
            public class and_it_has_the_factory : when_finding_a_factory_for_a_dependency
            {
                Establish c = () =>
                {
                    the_dependency = new SomeDependency();
                    the_factory = fake.an<ICreateASingleDependency>();

                    factories_by_type = new Dictionary<Type, ICreateASingleDependency>();
                    factories_by_type.Add(typeof(SomeDependency), the_factory);
                    the_factory.setup(x => x.create()).Return(the_dependency);

                    depends.on(factories_by_type);
                };

                Because b = () =>
                    result = sut.get_the_factory_that_can_create(typeof(SomeDependency));

                It should_return_the_factory_for_the_requested_dependency = () =>
                    result.ShouldEqual(the_factory);

                static ICreateASingleDependency result;
                static ICreateASingleDependency the_factory;
                static SomeDependency the_dependency;
                static IDictionary<Type, ICreateASingleDependency> factories_by_type;
            }

            public class and_it_does_not_have_the_factory : when_finding_a_factory_for_a_dependency
            {
                Establish c = () =>
                {
                    the_dependency = new SomeDependency();
                    the_factory = fake.an<ICreateASingleDependency>();
                    the_missing_factory = fake.an<ICreateASingleDependency>();

                    factories_by_type = new Dictionary<Type, ICreateASingleDependency>();

                    the_factory.setup(x => x.create()).Return(the_dependency);

                    depends.on(factories_by_type);
                    depends.on<CreateTheMissingDependencyFactory>(x =>
                    {
                        x.ShouldEqual(typeof(SomeDependency));
                        return the_missing_factory;
                    });
                };

                Because b = () =>
                    result = sut.get_the_factory_that_can_create(typeof(SomeDependency));

                It should_return_the_missing_dependency_factory = () =>
                    result.ShouldEqual(the_missing_factory);

                static ICreateASingleDependency result;
                static ICreateASingleDependency the_factory;
                static SomeDependency the_dependency;
                static IDictionary<Type, ICreateASingleDependency> factories_by_type;
                static ICreateASingleDependency the_missing_factory;
            }
        }
    }

    public class SomeDependency
    {
    }
}