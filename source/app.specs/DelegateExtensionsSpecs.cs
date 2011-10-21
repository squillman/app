using System;
using System.Data;
using System.Data.SqlClient;
using Machine.Specifications;
using app.infrastructure;
using developwithpassion.specifications.rhinomocks;

namespace app.specs
{
    [Subject(typeof(DelegateExtensions))]
    public class DelegateExtensionsSpecs
    {
        public abstract class concern : Observes
        {

        }

        public class when_a_value_returning_delegate_is_memoized : concern
        {
            Establish c = () =>
            {
                target = () => new SqlConnection();
            };

            Because b = () =>
                target = DelegateExtensions.memoize(target);

            It should_return_the_same_value_everytime_it_is_called = () =>
            {
                result = target();
                result.ShouldEqual(target());
            };

            static Func<IDbConnection> target;
            static IDbConnection result;
                
        }
    }
}
