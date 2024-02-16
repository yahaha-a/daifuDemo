using QFramework;

namespace daifuDemo
{
    public class OpenOrCloseBackpackCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            var uiGamePanelModel = this.GetModel<IUIGamePanelModel>();

            uiGamePanelModel.IfBackPackOpen.Value = !uiGamePanelModel.IfBackPackOpen.Value;
        }
    }
}