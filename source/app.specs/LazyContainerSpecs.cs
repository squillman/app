 using System.Data;
 using Machine.Specifications;
 using app.infrastructure.containers;
 using app.infrastructure.containers.simple;
 using developwithpassion.specifications.rhinomocks;
 using developwithpassion.specifications.extensions;

namespace app.specs
{  
    [Subject(typeof(LazyContainer))]  
    public class LazyContainerSpecs
    {
        public abstract class concern : Observes<IFetchDependencies,
                                            LazyContainer>
        {
        
        }

   
        public class when_fetching_a_dependency : concern
        {
            Establish c = () =>
            {
                the_connection = fake.an<IDbConnection>();
                container = fake.an<IFetchDependencies>();

                container.setup(x => x.an<IDbConnection>()).Return(the_connection);
                
            };

            Because b = () =>
            {
                concrete_sut.container = container;
                result = sut.an<IDbConnection>();
            };

            It should_delegate_fetching_to_its_lazily_initialized_container = () =>
                result.ShouldEqual(the_connection);

            static IFetchDependencies container;
            static IDbConnection result;
            static IDbConnection the_connection;
        }
    }
}
