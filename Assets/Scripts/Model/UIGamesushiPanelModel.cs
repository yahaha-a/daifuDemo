using QFramework;

namespace daifuDemo
{
    public interface IUIGamesushiPanelModel : IModel
    {
        BindableProperty<bool> IfUIsushiIngredientPanelOpen { get; }
        
        BindableProperty<bool> IfUIStaffManagePanelOpen { get; }
        
        BindableProperty<bool> IfUisushiMenuPanelOpen { get; }
        
        BindableProperty<bool> IfGoShipPanelOpen { get; }
        
        BindableProperty<string> SelectedMenuItemKey { get; }
        
        BindableProperty<bool> IfUIMenuPanelShow { get; }
        
        BindableProperty<bool> IfUISelectMenuAmountPanelShow { get; }
        
        BindableProperty<bool> IfUIUpgradeMenuPanelShow { get; }
        
        BindableProperty<int> CurrentSelectMenuAmount { get; }
        
        BindableProperty<int> MaxTodayMenuAmount { get; }
        
        BindableProperty<int> CurrentSelectTodayMenuItemNode { get; }
        
        BindableProperty<BackPackItemType> CurrentBackPackItemType { get; }
        
        BindableProperty<string> CurrentSelectsushiBackPackItemKey { get; }
        
        BindableProperty<bool> IfStaffRestaurantManagePanelShow { get; }
        
        BindableProperty<bool> IfStaffWaitingRoomManagePanelShow { get; }
        
        BindableProperty<bool> IfAppointStaffToKitchen { get; }
        
        BindableProperty<bool> IfAppointStaffToLobby { get; }
        
        BindableProperty<string> SelectStaffKey { get; }
        
        BindableProperty<bool> IfReplaceConfirmPanelShow { get; }
        
        BindableProperty<int> CurrentCookListNode { get; }
        
        BindableProperty<int> CurrentWaiterListNode { get; }
    }
    
    public class UIGamesushiPanelModel : AbstractModel, IUIGamesushiPanelModel
    {
        protected override void OnInit()
        {
            
        }

        public BindableProperty<bool> IfUIsushiIngredientPanelOpen { get; } = new BindableProperty<bool>(false);
        
        public BindableProperty<bool> IfUIStaffManagePanelOpen { get; } = new BindableProperty<bool>(false);

        public BindableProperty<bool> IfUisushiMenuPanelOpen { get; } = new BindableProperty<bool>(false);
        
        public BindableProperty<bool> IfGoShipPanelOpen { get; } = new BindableProperty<bool>(false);

        public BindableProperty<string> SelectedMenuItemKey { get; } = new BindableProperty<string>();
        
        public BindableProperty<bool> IfUIMenuPanelShow { get; } = new BindableProperty<bool>(false);
        
        public BindableProperty<bool> IfUISelectMenuAmountPanelShow { get; } = new BindableProperty<bool>(false);
        
        public BindableProperty<bool> IfUIUpgradeMenuPanelShow { get; } = new BindableProperty<bool>(false);

        public BindableProperty<int> CurrentSelectMenuAmount { get; } = new BindableProperty<int>(1);
        
        public BindableProperty<int> MaxTodayMenuAmount { get; } = new BindableProperty<int>(MenuItemConfig.MaxTodayMenuAmount);

        public BindableProperty<int> CurrentSelectTodayMenuItemNode { get; } = new BindableProperty<int>(0);

        public BindableProperty<BackPackItemType> CurrentBackPackItemType { get; } =
            new BindableProperty<BackPackItemType>(BackPackItemType.Fish);

        public BindableProperty<string> CurrentSelectsushiBackPackItemKey { get; } = new BindableProperty<string>();

        public BindableProperty<bool> IfStaffRestaurantManagePanelShow { get; } = new BindableProperty<bool>(true);

        public BindableProperty<bool> IfStaffWaitingRoomManagePanelShow { get; } = new BindableProperty<bool>(false);
        
        public BindableProperty<bool> IfAppointStaffToKitchen { get; } = new BindableProperty<bool>(false);
        
        public BindableProperty<bool> IfAppointStaffToLobby { get; } = new BindableProperty<bool>(false);
        
        public BindableProperty<string> SelectStaffKey { get; } = new BindableProperty<string>();
        
        public BindableProperty<bool> IfReplaceConfirmPanelShow { get; } = new BindableProperty<bool>(false);
        
        public BindableProperty<int> CurrentCookListNode { get; } = new BindableProperty<int>(1);
        
        public BindableProperty<int> CurrentWaiterListNode { get; } = new BindableProperty<int>(1);
    }
}