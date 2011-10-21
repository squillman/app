using Machine.Specifications;
using app.infrastructure.containers;
using app.tasks.startup;
using app.web.infrastructure;
using app.web.infrastructure.aspnet;
using developwithpassion.specifications.rhinomocks;
using developwithpassion.specifications.extensions;

namespace app.specs
{
    [Subject(typeof(StartTheApplication))]
    public class StartTheApplicationSpecs
    {
        public abstract class concern : Observes
        {

        }

        public class when_observation_name : concern
        {
            Because b = () =>
                StartTheApplication.run();

            It should_be_able_to_access_key_services = () =>
            {
                Container.fetch.an<IProcessRequests>().ShouldBeAn<FrontController>();
                Container.fetch.an<IFindCommandsThatCanProcessRequests>().ShouldBeAn<CommandRegistry>();
                Container.fetch.an<IDisplayInformation>().ShouldBeAn<WebFormReportEngine>();
            };

                    
        }
    }
}
