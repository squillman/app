using System;
using System.Collections.Generic;
using System.Data;
using Machine.Specifications;
using app.infrastructure.containers;
using app.infrastructure.containers.simple;
using developwithpassion.specifications.rhinomocks;
using developwithpassion.specifications.extensions;

namespace app.specs
{
    [Subject(typeof(SingleDependencyFactory))]
    public class SingleDependencyFactorySpecs
    {
        public abstract class concern : Observes<ICreateASingleDependency,
                                            SingleDependencyFactory>
        {

        }


        public class when_creating_dependencies : concern
        {

            public class and_creating_for_an_instance : when_creating_dependencies
            {
                Establish c = () =>
                                  {
                                      some_dependency = fake.an<MyDependency>();
                                      dependency_type = some_dependency.GetType();
                                      depends.on(dependency_type);
                                      depends.on(simple_dependency_factory);
                                  };

                private Because b = () =>
                                    sut.create();

                private It should_return_an_instance_of_the_specified_contract = () =>
                    result.ShouldBeOfType(typeof(SomeDependency));

                private static object result;
                private static MyDependency some_dependency;
                private static SingleDependencyFactory single_dependency_factory;
                private static Type dependency_type;
                private static SimpleDependencyFactory simple_dependency_factory;
            }

            public class MyDependency
            {
                
            }
        }
    }
}
