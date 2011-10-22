using System;
using app.infrastructure;
using app.infrastructure.containers;
using app.infrastructure.containers.simple;
using app.tasks.startup.pipeline;

namespace app.tasks.startup
{
    public class Factories
    {
        static Func<IRegisterComponentsIntoTheContainer> container_registration_factory = () =>
            new ContainerRegistrationFacility(new DependencyFactoriesFactory(new LazyContainer(),
                                                                             new GreediestContructorPicker()));

        public static Func<IRegisterComponentsIntoTheContainer> registration_factory = container_registration_factory.memoize();
    }

    public class Start
    {
        public static Func<Type, IComposeStartupChains> builder_factory = x =>
        {
            ICreateStartupPipelineCommands command_factory =
                new StartupPipelineCommandFactory(Factories.registration_factory());
            return new StartupPipelineBuilder(command_factory, command_factory.create_command_of(x));
        };

        public static IComposeStartupChains by<StartupElement>() where StartupElement : IPlayAPartInApplicationStartUp
        {
            return builder_factory(typeof(StartupElement));
        }

        public static void by_running_all_commands_in(string path_to_startup_file)
        {
            throw new NotImplementedException();
        }
    }
}