 using System.Security;
 using System.Security.Principal;
 using System.Threading;
 using Machine.Specifications;
 using app.web.application.catalogbrowsing;
 using app.web.infrastructure;
 using developwithpassion.specifications.rhinomocks;
 using developwithpassion.specifications.extensions;

namespace app.specs
{  
    [Subject(typeof(SecuredQuery<>))]  
    public class SecuredQuerySpecs
    {
        public abstract class concern : Observes<IFetchA<SomeReport>,
                                            SecuredQuery<SomeReport>>
        {
        
        }

   
        public class when_fetching_details : concern
        {
            public class and_they_match_the_security_constraints:when_fetching_details
            {
                Establish c = () =>
                {
                    the_report = new SomeReport();
                    request = fake.an<IContainRequestDetails>();
                    original = depends.on<IFetchA<SomeReport>>();
                    principal = fake.an<IPrincipal>();
                    depends.on<UserCriteria>(x => true);

                    original.setup(x => x.query_using(request)).Return(the_report);

                    spec.change(() => Thread.CurrentPrincipal).to(principal);
                };

                Because b = () =>
                    result = sut.query_using(request);


                It should_return_the_report_requested = () =>
                    result.ShouldEqual(the_report);


                static SomeReport result;
                static SomeReport the_report;
                static IContainRequestDetails request;
                static IFetchA<SomeReport> original;
                static IPrincipal principal;
            }

            public class and_they_dont_match_the_security_constraints:when_fetching_details
            {
                Establish c = () =>
                {
                    the_report = new SomeReport();
                    request = fake.an<IContainRequestDetails>();
                    original = depends.on<IFetchA<SomeReport>>();
                    principal = fake.an<IPrincipal>();
                    depends.on<UserCriteria>(x => false);

                    spec.change(() => Thread.CurrentPrincipal).to(principal);
                };

                Because b = () =>
                    spec.catch_exception(() => sut.query_using(request));


                It should_throw_a_security_exception = () =>
                    spec.exception_thrown.ShouldBeAn<SecurityException>();

                It should_not_have_invoked_the_original = () =>
                    original.never_received(x => x.query_using(request));
                    


                static SomeReport result;
                static SomeReport the_report;
                static IContainRequestDetails request;
                static IFetchA<SomeReport> original;
                static IPrincipal principal;
            }
                
        }
    }

    public class SomeReport
    {
    }
}
