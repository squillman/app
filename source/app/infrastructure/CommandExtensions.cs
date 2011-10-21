namespace app.infrastructure
{
    public static class CommandExtensions
    {
        public static IEncapsulateABehaviour followed_by(this IEncapsulateABehaviour the_first, IEncapsulateABehaviour the_second)
        {
            return new ChainedCommand(the_first, the_second);
        }
    }
}