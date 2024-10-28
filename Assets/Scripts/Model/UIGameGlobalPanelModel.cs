using QFramework;

namespace daifuDemo
{
    public interface IUIGameGlobalPanelModel : IModel
    {
        BindableProperty<bool> IfTaskPanelShow { get; }
        
        BindableProperty<int> CurrentShowObtainItemsCount { get; }
    }
    
    public class UIGameGlobalPanelModel : AbstractModel, IUIGameGlobalPanelModel
    {
        protected override void OnInit()
        {
            
        }

        public BindableProperty<bool> IfTaskPanelShow { get; } = new BindableProperty<bool>(false);
        
        public BindableProperty<int> CurrentShowObtainItemsCount { get; } = new BindableProperty<int>(0);
    }
}