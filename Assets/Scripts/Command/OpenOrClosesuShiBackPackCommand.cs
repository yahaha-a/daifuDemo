using QFramework;

namespace daifuDemo
{
    public class OpenOrClosesuShiBackPackCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            var uiGamePanelModel = this.GetModel<IUIGamePanelModel>();

            uiGamePanelModel.IfsushiUIPackOpen.Value = !uiGamePanelModel.IfsushiUIPackOpen.Value;
        }
    }
}