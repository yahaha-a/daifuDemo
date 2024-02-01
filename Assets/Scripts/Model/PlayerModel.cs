using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public enum PlayState
    {
        Swim,
        Aim,
        CatchFish,
        Attack
    }
    
    public interface IPlayerModel : IModel
    {
        BindableProperty<int> NumberOfFish { get; }
        
        BindableProperty<float> PlayerOxygen { get; }
        
        BindableProperty<float> InvincibleTime { get; }
        
        BindableProperty<bool> Invincibility { get; }
        
        BindableProperty<bool> IfLeft { get; }

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

        public BindableProperty<float> InvincibleTime { get; } =
            new BindableProperty<float>(Config.PlayerInvincibleTime);

        public BindableProperty<string> WeaponKey { get; } = new BindableProperty<string>(Config.FishForkKey);

        public BindableProperty<bool> Invincibility { get; } = new BindableProperty<bool>(false);

        public BindableProperty<bool> IfLeft { get; } = new BindableProperty<bool>(false);

        public BindableProperty<PlayState> State { get; } = new BindableProperty<PlayState>(PlayState.Swim);
    }
}