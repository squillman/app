using app.tasks.startup.pipeline;

namespace app.tasks.startup
{
    public class Start
    {
        public static void by<StartupElement>() where StartupElement : IPlayAPartInApplicationStartUp
        {
            throw new System.NotImplementedException();
        }
    }
}