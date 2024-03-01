using QFramework;

namespace daifuDemo
{
    public interface IPlayerSuShiModel : IModel
    {
        BindableProperty<bool> IfCanTakeFinishDish { get; }
        
        BindableProperty<bool> IfCanGiveCurrentDish { get; }
    }
    
    public class PlayerSuShiModel :AbstractModel, IPlayerSuShiModel
    {
        protected override void OnInit()
        {
            
        }

        public BindableProperty<bool> IfCanTakeFinishDish { get; } = new BindableProperty<bool>(false);
        
        public BindableProperty<bool> IfCanGiveCurrentDish { get; } = new BindableProperty<bool>(false);
    }
}