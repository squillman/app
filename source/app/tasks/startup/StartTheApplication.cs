using app.tasks.startup.pipeline;

namespace app.tasks.startup
{
    public class StartTheApplication
    {
        public static void run()
        {
            Start.by<ConfiguringTheContainer>()
                .then_by<ConfiguringTheFrontController>()
                .then_by<ConfiguringTheServiceLayer>()
                .finish_by<ConfiguringTheApplicationCommands>();
        }
    }
}