using QFramework;

namespace daifuDemo
{
    public class OpenOrCloseGoShipPanelCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            var uiGamesushiPanelModel = this.GetModel<IUIGamesushiPanelModel>();
            uiGamesushiPanelModel.IfGoShipPanelOpen.Value = !uiGamesushiPanelModel.IfGoShipPanelOpen.Value;
        }
    }
}