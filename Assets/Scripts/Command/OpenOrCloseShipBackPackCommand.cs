using QFramework;

namespace daifuDemo
{
    public class OpenOrCloseShipBackPackCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            var uiGameShipPanelModel = this.GetModel<IUIGameShipPanelModel>();

            uiGameShipPanelModel.IfShipUIPackOpen.Value = !uiGameShipPanelModel.IfShipUIPackOpen.Value;
        }
    }
}