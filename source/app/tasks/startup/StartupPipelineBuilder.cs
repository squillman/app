using System;
using app.infrastructure;
using app.tasks.startup.pipeline;

namespace app.tasks.startup
{
    public class StartupPipelineBuilder : IComposeStartupChains
    {
        public ICreateStartupPipelineCommands command_factory;
        public IEncapsulateABehaviour command;

        public StartupPipelineBuilder(ICreateStartupPipelineCommands command_factory,
                                      IEncapsulateABehaviour command)
        {
            this.command_factory = command_factory;
            this.command = command;
        }

        public IComposeStartupChains then_by<NextStartupElement>()
            where NextStartupElement : IPlayAPartInApplicationStartUp
        {
            return new StartupPipelineBuilder(command_factory, compose_command(typeof(NextStartupElement)));
        }

        IEncapsulateABehaviour compose_command(Type next_command_type)
        {
            return command.followed_by(command_factory.create_command_of(next_command_type));
        }

        public void finish_by<FinalElement>() where FinalElement : IPlayAPartInApplicationStartUp
        {
            compose_command(typeof(FinalElement)).run();
        }
    }
}