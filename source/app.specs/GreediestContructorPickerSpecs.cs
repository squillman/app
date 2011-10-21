using System.Data;
using System.Reflection;
using Machine.Specifications;
using app.infrastructure.containers;
using app.specs.utility;
using developwithpassion.specifications.rhinomocks;

namespace app.specs
{
    [Subject(typeof(GreediestContructorPicker))]
    public class GreediestContructorPickerSpecs
    {
        public abstract class concern : Observes<IChooseTheConstructorForAType,
                                            GreediestContructorPicker>
        {
        }

        public class when_picking_a_ctor_on_a_type : concern
        {
            Establish c = () =>
            {
                the_greediest_ctor =
                    ObjectFactory.expressions.to_target<OurTypeWithDependencies>().get_constructor(
                        () => new OurTypeWithDependencies(null, null, null));
            };

            Because b = () =>
                result = sut.get_the_applicable_constructor_on(typeof(OurTypeWithDependencies));

            It should_return_the_greediest = () =>
                result.ShouldEqual(the_greediest_ctor);

            static ConstructorInfo result;
            static ConstructorInfo the_greediest_ctor;
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

            public OurTypeWithDependencies(IDbConnection connection, IDbCommand command)
            {
                this.connection = connection;
                this.command = command;
            }
        }
    }
}