using QFramework;

namespace daifuDemo
{
    public interface IUIGamesushiPanelModel : IModel
    {
        BindableProperty<bool> IfUIsushiIngredientPanelOpen { get; }
    }
    
    public class UIGamesushiPanelModel : AbstractModel, IUIGamesushiPanelModel
    {
        protected override void OnInit()
        {
            
        }

        public BindableProperty<bool> IfUIsushiIngredientPanelOpen { get; } = new BindableProperty<bool>(false);
    }
}