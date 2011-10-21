using System;

namespace app.tasks.startup.pipeline
{
    public interface ICreateStartupPipelineCommands
    {
        IPlayAPartInApplicationStartUp create_command_of(Type command_type);
    }
}