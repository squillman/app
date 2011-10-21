 using Machine.Specifications;
 using app.infrastructure.containers;
 using developwithpassion.specifications.rhinomocks;
 using developwithpassion.specifications.extensions;

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

            public class and_it_has_the_factory:when_finding_a_factory_for_a_dependency
            {
                Establish c = () =>
                {
                    the_dependency = new SomeDependency();
                    the_factory = fake.an<ICreateASingleDependency>();
                    the_factory.setup(x => x.create()).Return(the_dependency);
                };

                Because b = () =>
                    result = sut.get_the_factory_that_can_create(typeof(SomeDependency));

                It should_return_the_factory_for_the_requested_dependency = () =>
                    result.ShouldEqual(the_factory);


                static ICreateASingleDependency result;
                static ICreateASingleDependency the_factory;
                static SomeDependency the_dependency;
            }
                
        }
    }

    public class SomeDependency
    {
    }
}
