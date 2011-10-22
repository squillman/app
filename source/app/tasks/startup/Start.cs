using System;
using System.IO;
using app.infrastructure;
using app.infrastructure.containers;
using app.infrastructure.containers.simple;
using app.tasks.startup.pipeline;
using System.Linq;

namespace app.tasks.startup
{
    public class Factories
    {
        static Func<IRegisterComponentsIntoTheContainer> container_registration_factory = () =>
            new ContainerRegistrationFacility(new DependencyFactoriesFactory(new LazyContainer(),
                                                                             new GreediestContructorPicker()));

        public static Func<IRegisterComponentsIntoTheContainer> registration_factory = container_registration_factory.memoize();

        public static Func<ICreateStartupPipelineCommands> command_factory = () => 
                new StartupPipelineCommandFactory(Factories.registration_factory());
    }

    public class Start
    {
        public static Func<Type, IComposeStartupChains> builder_factory = x =>
        {
            ICreateStartupPipelineCommands command_factory = Factories.command_factory();
            return new StartupPipelineBuilder(command_factory, command_factory.create_command_of(x));
        };

        public static IComposeStartupChains by<StartupElement>() where StartupElement : IPlayAPartInApplicationStartUp
        {
            return builder_factory(typeof(StartupElement));
        }

        public static void by_running_pipeline_sequence_defined_in(string path_to_startup_file)
        {
            File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,path_to_startup_file))
                .Select(x => typeof(IPlayAPartInApplicationStartUp).Assembly.GetType(x))
                .Select(x => Factories.command_factory().create_command_of(x))
                .each(x => x.run());
        }
    }
}