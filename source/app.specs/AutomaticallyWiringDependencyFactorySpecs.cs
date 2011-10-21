using System.Data;
using Machine.Specifications;
using app.infrastructure.containers;
using app.infrastructure.containers.simple;
using app.specs.utility;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;

namespace app.specs
{
    [Subject(typeof(AutomaticallyWiringDependencyFactory))]
    public class AutomaticallyWiringDependencyFactorySpecs
    {
        public abstract class concern : Observes<ICreateASingleDependency,
                                            AutomaticallyWiringDependencyFactory>
        {
        }

        public class when_creating_a_dependency : concern
        {
            Establish c = () =>
            {
                the_connection = fake.an<IDbConnection>();
                the_command = fake.an<IDbCommand>();
                the_reader = fake.an<IDataReader>();

                container = depends.on<IFetchDependencies>();
                depends.on(typeof(OurTypeWithDependencies));
                constructor_picker = depends.on<IChooseTheConstructorForAType>();

                constructor_picker.setup(x => x.get_the_applicable_constructor_on(typeof(OurTypeWithDependencies))).
                    Return(ObjectFactory.expressions.to_target<OurTypeWithDependencies>().get_constructor(() => new OurTypeWithDependencies(null,null,null)));

                container.setup(x => x.an(typeof(IDbConnection))).Return(the_connection);
                container.setup(x => x.an(typeof(IDbCommand))).Return(the_command);
                container.setup(x => x.an(typeof(IDataReader))).Return(the_reader);
            };

            Because b = () =>
                result = sut.create();

            It should_return_the_dependency_with_all_of_its_depenendencies_provided = () =>
            {
                var item = result.ShouldBeAn<OurTypeWithDependencies>();
                item.connection.ShouldEqual(the_connection);
                item.command.ShouldEqual(the_command);
                item.reader.ShouldEqual(the_reader);
            };

            static object result;
            static IDbConnection the_connection;
            static IDbCommand the_command;
            static IDataReader the_reader;
            static IFetchDependencies container;
            static IChooseTheConstructorForAType constructor_picker;
        }

        public class OurTypeWithDependencies
        {
            public IDbConnection connection;
            public IDbCommand command;
            public IDataReader reader;

            public OurTypeWithDependencies(IDbConnection connection)
            {
            }

            public OurTypeWithDependencies(IDbConnection connection, IDbCommand command, IDataReader reader)
            {
                this.connection = connection;
                this.command = command;
                this.reader = reader;
            }
        }
    }
}