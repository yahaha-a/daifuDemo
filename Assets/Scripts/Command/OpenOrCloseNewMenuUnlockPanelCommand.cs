using QFramework;

namespace daifuDemo
{
    public class OpenOrCloseNewMenuUnlockPanelCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            var uiGamesushiModel = this.GetModel<IUIGamesushiPanelModel>();

            uiGamesushiModel.IfUINewMenuUnlockPanelOpen.Value = !uiGamesushiModel.IfUINewMenuUnlockPanelOpen.Value;
        }
    }
}