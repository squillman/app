using System;
using System.Data;
using System.Data.SqlClient;
using Machine.Specifications;
using app.infrastructure.containers;
using developwithpassion.specifications.rhinomocks;

namespace app.specs
{
    [Subject(typeof(SimpleDependencyFactory))]
    public class SimpleDependencyFactorySpecs
    {
        public abstract class concern : Observes<ICreateASingleDependency,
                                            SimpleDependencyFactory>
        {
        }

        public class when_creating_a_dependency : concern
        {
            Establish c = () =>
            {
                the_connection = new SqlConnection();
                depends.on<Func<object>>(() => the_connection);
            };

            Because b = () =>
                result = sut.create();

            It should_return_the_instance_created_by_its_creation_delegate = () =>
                result.ShouldEqual(the_connection);

            static object result;
            static IDbConnection the_connection;
        }
    }
}