namespace app.web.infrastructure.stubs
{
    public class StubMissingCommand:IRepresentACommandThatIsNotYetSupported
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