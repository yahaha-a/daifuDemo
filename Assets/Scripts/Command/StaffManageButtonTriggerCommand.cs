using QFramework;

namespace daifuDemo
{
    public class StaffManageButtonTriggerCommand : AbstractCommand
    {
        protected override void OnExecute()
        {
            var uiGamesushiPanelModel = this.GetModel<IUIGamesushiPanelModel>();

            uiGamesushiPanelModel.IfStaffRestaurantManagePanelShow.Value =
                !uiGamesushiPanelModel.IfStaffRestaurantManagePanelShow.Value;

            uiGamesushiPanelModel.IfStaffWaitingRoomManagePanelShow.Value =
                !uiGamesushiPanelModel.IfStaffWaitingRoomManagePanelShow.Value;
        }
    }
}