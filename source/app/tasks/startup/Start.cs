using System;
using app.tasks.startup.pipeline;

namespace app.tasks.startup
{
    public class Start
    {
        public static Func<Type, IComposeStartupChains> builder_factory = x =>
        {
            throw new NotImplementedException();
        };

        public static IComposeStartupChains by<StartupElement>() where StartupElement : IPlayAPartInApplicationStartUp
        {
            return builder_factory(typeof(StartupElement));
        }
    }
}