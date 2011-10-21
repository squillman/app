using app.infrastructure.containers.simple;
using app.tasks.stubs;

namespace app.tasks.startup.pipeline
{
    public class ConfiguringTheServiceLayer:IPlayAPartInApplicationStartUp
    {
        IRegisterComponentsIntoTheContainer registration;

        public ConfiguringTheServiceLayer(IRegisterComponentsIntoTheContainer registration)
        {
            this.registration = registration;
        }

        public void run()
        {

            registration.register<IProvideInformationAboutTheStore, StubStoreDirectory>();
        }
    }
}