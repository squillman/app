using System;
using System.Collections.Generic;
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
        private static Func<IRegisterComponentsIntoTheContainer> container_registration_factory = () =>
                                                                                                  new ContainerRegistrationFacility
                                                                                                      (new DependencyFactoriesFactory
                                                                                                           (new LazyContainer
                                                                                                                (),
                                                                                                            new GreediestContructorPicker
                                                                                                                ()));

        public static Func<IRegisterComponentsIntoTheContainer> registration_factory =
            container_registration_factory.memoize();

        public static Func<ICreateStartupPipelineCommands> command_factory = () =>
                                                                             new StartupPipelineCommandFactory(
                                                                                 Factories.registration_factory());
    }

    public class Start
    {
        public static Func<Type, IComposeStartupChains> builder_factory = x =>
                                                                              {
                                                                                  ICreateStartupPipelineCommands
                                                                                      command_factory =
                                                                                          Factories.command_factory();
                                                                                  return
                                                                                      new StartupPipelineBuilder(
                                                                                          command_factory,
                                                                                          command_factory.
                                                                                              create_command_of(x));
                                                                              };

        public static IComposeStartupChains by<StartupElement>() where StartupElement : IPlayAPartInApplicationStartUp
        {
            return builder_factory(typeof (StartupElement));
        }

        public static void by_running_pipeline_sequence_defined_in(string path_to_startup_file)
        {
            IEnumerable<string> command_names = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path_to_startup_file));
            IEnumerable<Type> command_types = command_names.Select(x => StringToTypeMapper.map(x));


                command_types.Select(x => Factories.command_factory().create_command_of(x))
                .each(x => x.run());
        }

        public static class PipelineStartupCommandNameReader
        {
            
        }

        public static class StringToTypeMapper
        {
            public static Type map(string command_name)
            {
                try
                {
                    return typeof(IPlayAPartInApplicationStartUp).Assembly.GetType(command_name);
                }
                catch (Exception ex)
                {
                    throw new StringToTypeMapperException();
                }
            }
        }
    }
}