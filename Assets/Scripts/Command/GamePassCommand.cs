using QFramework;

namespace daifuDemo
{
    public class GamePassCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            Events.GamePass?.Trigger();
        }
    }
}