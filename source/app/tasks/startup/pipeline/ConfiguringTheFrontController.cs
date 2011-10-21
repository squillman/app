using System.Collections.Generic;
using System.Web;
using System.Web.Compilation;
using app.infrastructure.containers.simple;
using app.web.infrastructure;
using app.web.infrastructure.aspnet;
using app.web.infrastructure.stubs;

namespace app.tasks.startup.pipeline
{
    public class ConfiguringTheFrontController:IPlayAPartInApplicationStartUp
    {
        IRegisterComponentsIntoTheContainer registration;

        public ConfiguringTheFrontController(IRegisterComponentsIntoTheContainer registration)
        {
            this.registration = registration;
        }

        public void run()
        {
            registration.register<ICreateRequests, StubRequestFactory>();
            registration.register<IProcessRequests, FrontController>();
            registration.register<IEnumerable<IProcessOneSpecificTypeOfRequest>, StubSetOfCommands>();
            registration.register<IDisplayInformation, WebFormReportEngine>();
            registration.register<ICreateTemplateInstances, ASPXTemplateFactory>();
            registration.register<IFindPathsToTemplates, StubAspxPathRegistry>();
            registration.register<IFindCommandsThatCanProcessRequests, CommandRegistry>();
            registration.register<IRepresentACommandThatIsNotYetSupported, StubMissingCommand>();
            registration.register_instance<GetTheCurrentlyExecutingContext>(() => HttpContext.Current);
            registration.register_instance<PageFactory>(BuildManager.CreateInstanceFromVirtualPath);
        } 
    }
}