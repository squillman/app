using app.infrastructure.containers.simple;
using app.web.application.catalogbrowsing;

namespace app.tasks.startup.pipeline
{
    public class ConfiguringTheApplicationCommands:IPlayAPartInApplicationStartUp
    {
        IRegisterComponentsIntoTheContainer registration;

        public ConfiguringTheApplicationCommands(IRegisterComponentsIntoTheContainer registration)
        {
            this.registration = registration;
        }

        public void run()
        {
            registration.register<ViewTheMainDepartments>();
            registration.register<ViewTheDepartmentsInADepartment>();
            registration.register<ViewTheProductsInADepartment>();
        }
    }
}