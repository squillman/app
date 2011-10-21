namespace app.web.infrastructure.stubs
{
    public class StubMissingCommand:IProcessOneSpecificTypeOfRequest
    {
        public void process(IContainRequestDetails request)
        {
        }

        public bool can_handle(IContainRequestDetails request)
        {
            return false;
        }
    }
}