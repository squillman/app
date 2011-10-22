namespace app.tasks.startup
{
    public class StartTheApplication
    {
        public static void run()
        {
            Start.by_running_pipeline_sequence_defined_in("startup_order.txt");
        }
    }
}