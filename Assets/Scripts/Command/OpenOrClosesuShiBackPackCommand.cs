using QFramework;

namespace daifuDemo
{
    public class OpenOrClosesuShiBackPackCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            var uiGameShipPanelModel = this.GetModel<IUIGameShipPanelModel>();

            uiGameShipPanelModel.IfsushiUIPackOpen.Value = !uiGameShipPanelModel.IfsushiUIPackOpen.Value;
        }
    }
}