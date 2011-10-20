using Machine.Specifications;
using app.infrastructure.containers;
using developwithpassion.specifications.rhinomocks;

namespace app.specs
{
    [Subject(typeof (ContainerFacade))]
    public class ContainerFacadeSpecs
    {
        public abstract class concern : Observes<IFetchDependencies, ContainerFacade>
        {
    }

        public class when_fetching_a_dependent_instance : concern
        {
            Establish c = () =>
            {
               
  
            };

            Because b = () => result = sut.an<AClassWithDependencies>();

            It should_return_an_instance_of_the_class_with_all_its_dependencies = () =>
                result.ShouldBeTheSameAs(the_class_with_dependencies);


            static AClassWithDependencies result;
            static AClassWithDependencies the_class_with_dependencies;
        }
    }

    public class AClassWithDependencies
    {
        DependsOnA dependsOnA { get; set; }
    }

    public class DependsOnA
    {
        
    }
}