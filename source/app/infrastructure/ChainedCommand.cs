namespace app.infrastructure
{
    public class ChainedCommand : IEncapsulateABehaviour
    {
        public IEncapsulateABehaviour first;
        public IEncapsulateABehaviour second;

        public ChainedCommand(IEncapsulateABehaviour first, IEncapsulateABehaviour second)
        {
            this.first = first;
            this.second = second;
        }

        public void run()
        {
            first.run();
            second.run();
        }
    }
}