using System;
using app.infrastructure.containers.simple;

namespace app.tasks.startup.pipeline
{
    public class StartupPipelineCommandFactory : ICreateStartupPipelineCommands
    {
        IRegisterComponentsIntoTheContainer registration_facility;

        public StartupPipelineCommandFactory(IRegisterComponentsIntoTheContainer registration_facility)
        {
            this.registration_facility = registration_facility;
        }

        public IPlayAPartInApplicationStartUp create_command_of(Type command_type)
        {
            try
            {
                return (IPlayAPartInApplicationStartUp) Activator.CreateInstance(command_type, registration_facility);
            }
            catch (Exception e)
            {
                throw new StartupCommandPolicyViolationException(command_type, e);
            }
        }
    }
}