using QFramework;

namespace daifuDemo
{
    public class PlayerTurnToLeft : AbstractCommand
    {
        protected override void OnExecute()
        {
            Events.PlayerVeer.Trigger(true);
        }
    }
}