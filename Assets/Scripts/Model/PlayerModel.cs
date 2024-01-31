using QFramework;

namespace daifuDemo
{
    public interface IPlayerModel : IModel
    {
        BindableProperty<int> NumberOfFish { get; }
        
        BindableProperty<float> PlayerOxygen { get; }
        
        BindableProperty<PlayState> State { get; }
        
        BindableProperty<string> WeaponKey { get; }
    }
    
    public class PlayerModel : AbstractModel, IPlayerModel
    {
        protected override void OnInit()
        {
            
        }

        public BindableProperty<int> NumberOfFish { get; } = new BindableProperty<int>(0);

        public BindableProperty<float> PlayerOxygen { get; } = new BindableProperty<float>(Config.PlayerOxygen);

        public BindableProperty<PlayState> State { get; } = new BindableProperty<PlayState>(PlayState.Attack);

        public BindableProperty<string> WeaponKey { get; } = new BindableProperty<string>(Config.FishForkKey);
    }
}