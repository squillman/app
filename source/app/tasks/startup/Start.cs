using System;
using app.infrastructure.containers;
using app.infrastructure.containers.simple;
using app.tasks.startup.pipeline;

namespace app.tasks.startup
{
    public class Factories
    {
        public static IRegisterComponentsIntoTheContainer create_registration_facility()
        {
            return new ContainerRegistrationFacility(new DependencyFactoriesFactory(new LazyContainer(), new GreediestContructorPicker()));
        }

    }
    public class Start
    {
        public static Func<Type, IComposeStartupChains> builder_factory = x =>
        {
            ICreateStartupPipelineCommands command_factory = new StartupPipelineCommandFactory(Factories.create_registration_facility());
            return new StartupPipelineBuilder(command_factory, command_factory.create_command_of(x));
        };

        public static IComposeStartupChains by<StartupElement>() where StartupElement : IPlayAPartInApplicationStartUp
        {
            return builder_factory(typeof(StartupElement));
        }
    }
}