using QFramework;

namespace daifuDemo
{
    public class PlayerTurnToRight : AbstractCommand
    {
        protected override void OnExecute()
        {
            Events.PlayerVeer.Trigger(false);
        }
    }
}