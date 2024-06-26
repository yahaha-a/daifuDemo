using QFramework;

namespace daifuDemo
{
    public interface IUIGamePanelModel : IModel
    {
        BindableProperty<bool> IfBackPackOpen { get; set; }
        
        BindableProperty<bool> IfUIHarvestPanelShow { get; }
        
        BindableProperty<bool> IfCatchFishPanelShow { get; }
    }
    
    public class UIGamePanelModel : AbstractModel, IUIGamePanelModel
    {
        protected override void OnInit()
        {
            
        }

        public BindableProperty<bool> IfBackPackOpen { get; set; } = new BindableProperty<bool>(false);
        
        public BindableProperty<bool> IfUIHarvestPanelShow { get; } = new BindableProperty<bool>(false);
        
        public BindableProperty<bool> IfCatchFishPanelShow { get; } = new BindableProperty<bool>(false);
    }
}