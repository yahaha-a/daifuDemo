using QFramework;

namespace daifuDemo
{
    public interface IUIGamesushiPanelModel : IModel
    {
        BindableProperty<bool> IfUIsushiIngredientPanelOpen { get; }
        
        BindableProperty<bool> IfUisushiMenuPanelOpen { get; }
        
        BindableProperty<string> SelectedMenuItemKey { get; }
        
        BindableProperty<bool> IfUIMenuPanelShow { get; }
    }
    
    public class UIGamesushiPanelModel : AbstractModel, IUIGamesushiPanelModel
    {
        protected override void OnInit()
        {
            
        }

        public BindableProperty<bool> IfUIsushiIngredientPanelOpen { get; } = new BindableProperty<bool>(false);

        public BindableProperty<bool> IfUisushiMenuPanelOpen { get; } = new BindableProperty<bool>(false);

        public BindableProperty<string> SelectedMenuItemKey { get; } = new BindableProperty<string>("");
        
        public BindableProperty<bool> IfUIMenuPanelShow { get; } = new BindableProperty<bool>(false);
    }
}