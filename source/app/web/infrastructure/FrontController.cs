namespace app.web.infrastructure
{
    public class FrontController : IProcessRequests
    {
        private IFindCommandsThatCanProcessRequests command_registry;

        public FrontController(IFindCommandsThatCanProcessRequests commandRegistry)
        {
            command_registry = commandRegistry;
        }

        public void process(IContainRequestDetails a_new_request)
        {
            IProcessOneSpecificTypeOfRequest request_command =
                command_registry.get_the_command_that_can_process(a_new_request);
            request_command.process(a_new_request);
        }
    }
}