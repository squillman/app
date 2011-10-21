using app.tasks.startup.pipeline;

namespace app.tasks.startup
{
    public interface IComposeStartupChains
    {
        IComposeStartupChains then_by<NextElement>() where NextElement : IPlayAPartInApplicationStartUp;
        void finish_by<FinalElement>() where FinalElement : IPlayAPartInApplicationStartUp;
    }
}