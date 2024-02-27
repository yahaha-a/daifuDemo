using QFramework;

namespace daifuDemo
{
    public class OpenOrClosesushiMenuPanel : AbstractCommand
    {
        protected override void OnExecute()
        {
            var uiGameShipPanelModel = this.GetModel<IUIGamesushiPanelModel>();

            uiGameShipPanelModel.IfUisushiMenuPanelOpen.Value = !uiGameShipPanelModel.IfUisushiMenuPanelOpen.Value;
        }
    }
}