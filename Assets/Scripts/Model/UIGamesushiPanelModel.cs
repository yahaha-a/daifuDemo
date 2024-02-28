using QFramework;

namespace daifuDemo
{
    public interface IUIGamesushiPanelModel : IModel
    {
        BindableProperty<bool> IfUIsushiIngredientPanelOpen { get; }
        
        BindableProperty<bool> IfUisushiMenuPanelOpen { get; }
        
        BindableProperty<string> SelectedMenuItemKey { get; }
        
        BindableProperty<bool> IfUIMenuPanelShow { get; }
        
        BindableProperty<bool> IfUISelectMenuAmountPanelShow { get; }
        
        BindableProperty<bool> IfUIUpgradeMenuPanelShow { get; }
        
        BindableProperty<int> CurrentSelectMenuAmount { get; }
    }
    
    public class UIGamesushiPanelModel : AbstractModel, IUIGamesushiPanelModel
    {
        protected override void OnInit()
        {
            
        }

        public BindableProperty<bool> IfUIsushiIngredientPanelOpen { get; } = new BindableProperty<bool>(false);

        public BindableProperty<bool> IfUisushiMenuPanelOpen { get; } = new BindableProperty<bool>(false);

        public BindableProperty<string> SelectedMenuItemKey { get; } = new BindableProperty<string>();
        
        public BindableProperty<bool> IfUIMenuPanelShow { get; } = new BindableProperty<bool>(false);
        
        public BindableProperty<bool> IfUISelectMenuAmountPanelShow { get; } = new BindableProperty<bool>(false);
        
        public BindableProperty<bool> IfUIUpgradeMenuPanelShow { get; } = new BindableProperty<bool>(false);
        
        public BindableProperty<int> CurrentSelectMenuAmount { get; } = new BindableProperty<int>(1);
    }
}