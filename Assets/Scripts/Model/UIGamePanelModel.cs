using QFramework;

namespace daifuDemo
{
    public interface IUIGamePanelModel : IModel
    {
        BindableProperty<bool> IfBackPackOpen { get; set; }
        
        BindableProperty<bool> IfsushiUIPackOpen { get; set; }
    }
    
    public class UIGamePanelModel : AbstractModel, IUIGamePanelModel
    {
        protected override void OnInit()
        {
            
        }

        public BindableProperty<bool> IfBackPackOpen { get; set; } = new BindableProperty<bool>(false);

        public BindableProperty<bool> IfsushiUIPackOpen { get; set; } = new BindableProperty<bool>(false);
    }
}