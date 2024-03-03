using System.Threading;
using QFramework;

namespace daifuDemo
{
    public class OpenOrCloseStaffManagePanelCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            var iUIGameSushiPanelModel = this.GetModel<IUIGamesushiPanelModel>();
            iUIGameSushiPanelModel.IfUIStaffManagePanelOpen.Value =
                !iUIGameSushiPanelModel.IfUIStaffManagePanelOpen.Value;
        }
    }
}