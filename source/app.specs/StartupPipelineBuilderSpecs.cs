using System;
using System.Collections.Generic;
using Machine.Specifications;
using app.infrastructure;
using app.tasks.startup;
using app.tasks.startup.pipeline;
using developwithpassion.specifications.extensions;
using developwithpassion.specifications.rhinomocks;

namespace app.specs
{
    [Subject(typeof(StartupPipelineBuilder))]
    public class StartupPipelineBuilderSpecs
    {
        public abstract class concern : Observes<IComposeStartupChains,
                                            StartupPipelineBuilder>
        {
        }

        public class when_specifying_a_command_to_add_to_the_chain : concern
        {
            Establish c = () =>
            {
                first_command = fake.an<IPlayAPartInApplicationStartUp>();
                the_command = fake.an<IPlayAPartInApplicationStartUp>();
                command_factory = depends.on<ICreateStartupPipelineCommands>();

                depends.on(first_command);

                command_factory.setup(x => x.create_command_of(typeof(NextCommand)))
                    .Return(the_command);
            };

            Because b = () =>
                result = sut.then_by<NextCommand>();

            It should_return_a_new_builder_initialized_with_the_correct_chained_command = () =>
            {
                result.ShouldNotEqual(sut);
                var item = result.ShouldBeAn<StartupPipelineBuilder>();
                item.command.ShouldBeAn<ChainedCommand>();
                item.command_factory.ShouldEqual(command_factory);
            };

            static IList<IPlayAPartInApplicationStartUp> all_commands;
            static IPlayAPartInApplicationStartUp the_command;
            static ICreateStartupPipelineCommands command_factory;
            static IComposeStartupChains result;
            static IPlayAPartInApplicationStartUp first_command;
        }

        public class when_specifying_the_last_command : concern
        {
            Establish c = () =>
            {
                order_ran = new List<Type>();
                first_command = new FirstCommand {commands = order_ran};
                final_command = new NextCommand {commands = order_ran};
                depends.on(first_command);

                command_factory = depends.on<ICreateStartupPipelineCommands>();

                command_factory.setup(x => x.create_command_of(typeof(NextCommand)))
                    .Return(final_command);
            };

            Because b = () =>
                sut.finish_by<NextCommand>();

            It should_run_all_of_the_commands_in_the_correct_order = () =>
                order_ran.ShouldContainOnlyInOrder(typeof(FirstCommand), typeof(NextCommand));

            static ICreateStartupPipelineCommands command_factory;
            static IPlayAPartInApplicationStartUp first_command;
            static IPlayAPartInApplicationStartUp final_command;
            static List<Type> order_ran;
        }

        public class FirstCommand : IPlayAPartInApplicationStartUp
        {
            public IList<Type> commands;

            public void run()
            {
                commands.Add(this.GetType());
            }
        }

        public class NextCommand : IPlayAPartInApplicationStartUp
        {
            public IList<Type> commands;

            public void run()
            {
                commands.Add(this.GetType());
            }
        }
    }
}