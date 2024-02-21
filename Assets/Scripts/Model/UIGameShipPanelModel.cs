using QFramework;

namespace daifuDemo
{
    public interface IUIGameShipPanelModel : IModel
    {
        BindableProperty<bool> IfGoToHomePanelOpen { get; }
        
        BindableProperty<bool> IfGotoSeaPanelOpen { get; }
    }
    
    public class UIGameShipPanelModel : AbstractModel, IUIGameShipPanelModel
    {
        protected override void OnInit()
        {
            
        }

        public BindableProperty<bool> IfGoToHomePanelOpen { get; } = new BindableProperty<bool>(false);

        public BindableProperty<bool> IfGotoSeaPanelOpen { get; } = new BindableProperty<bool>(false);
    }
}