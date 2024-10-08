using QFramework;

namespace daifuDemo
{
    public interface IUIGameShipPanelModel : IModel
    {
        BindableProperty<bool> IfGoToHomePanelOpen { get; }
        
        BindableProperty<bool> IfGotoSeaPanelOpen { get; }
        
        BindableProperty<bool> IfShipUIPackOpen { get; }
        
        BindableProperty<bool> IfItemInfoShow { get; }
        
        BindableProperty<string> CurrentSelectMapName { get; }
        
        BindableProperty<string> CurrentMapName { get; }
    }
    
    public class UIGameShipPanelModel : AbstractModel, IUIGameShipPanelModel
    {
        protected override void OnInit()
        {
            
        }

        public BindableProperty<bool> IfGoToHomePanelOpen { get; } = new BindableProperty<bool>(false);

        public BindableProperty<bool> IfGotoSeaPanelOpen { get; } = new BindableProperty<bool>(false);
        
        public BindableProperty<bool> IfShipUIPackOpen { get; set; } = new BindableProperty<bool>(false);

        public BindableProperty<bool> IfItemInfoShow { get; } = new BindableProperty<bool>(false);
        
        public BindableProperty<string> CurrentSelectMapName { get; } = new BindableProperty<string>(null);

        public BindableProperty<string> CurrentMapName { get; } = new BindableProperty<string>(null);
    }
}