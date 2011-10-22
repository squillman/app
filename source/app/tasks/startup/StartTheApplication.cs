namespace app.tasks.startup
{
    public class StartTheApplication
    {
        public static void run()
        {
            Start.by_running_all_commands_in("startup_order.txt");
        }
    }
}