using QFramework;

namespace daifuDemo
{
    public interface IUIGameGlobalPanelModel : IModel
    {
        BindableProperty<bool> IfTaskPanelShow { get; }
    }
    
    public class UIGameGlobalPanelModel : AbstractModel, IUIGameGlobalPanelModel
    {
        protected override void OnInit()
        {
            
        }

        public BindableProperty<bool> IfTaskPanelShow { get; } = new BindableProperty<bool>(false);
    }
}