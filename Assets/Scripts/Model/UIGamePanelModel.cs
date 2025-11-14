using QFramework;

namespace daifuDemo
{
    public enum CounterPanelState
    {
        Hide,
        CatchFish,
        OpenTreasure,
        Reloading
    }
    
    public interface IUIGamePanelModel : IModel
    {
        BindableProperty<bool> IfBackPackOpen { get; set; }
        
        BindableProperty<bool> IfUIHarvestPanelShow { get; }
        
        BindableProperty<CounterPanelState> CurrentCounterPanelState { get; set; }
        
        BindableProperty<float> CurrentCounter { get; set; }
        
        BindableProperty<string> CurrentSelectItemKey { get; }
    }
    
    public class UIGamePanelModel : AbstractModel, IUIGamePanelModel
    {
        protected override void OnInit()
        {
            
        }

        public BindableProperty<bool> IfBackPackOpen { get; set; } = new BindableProperty<bool>(false);
        
        public BindableProperty<bool> IfUIHarvestPanelShow { get; } = new BindableProperty<bool>(false);

        public BindableProperty<CounterPanelState> CurrentCounterPanelState { get; set; } =
            new BindableProperty<CounterPanelState>(CounterPanelState.Hide);

        public BindableProperty<float> CurrentCounter { get; set; } = new BindableProperty<float>(0f);

        public BindableProperty<string> CurrentSelectItemKey { get; } = new BindableProperty<string>(null);
    }
}