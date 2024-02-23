using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public enum PlayState
    {
        Swim,
        Aim,
        CatchFish,
        Attack,
        OpenTreasureChests,
        PickUp,
        PickUpEd
    }

    public enum WeaponTypes
    {
        FishFork,
        Gun,
        MeleeWeapon
    }
    
    public interface IPlayerModel : IModel
    {
        BindableProperty<int> NumberOfFish { get; }
        
        BindableProperty<float> PlayerOxygen { get; }
        
        BindableProperty<float> InvincibleTime { get; }
        
        BindableProperty<bool> Invincibility { get; }
        
        BindableProperty<bool> IfLeft { get; }

        BindableProperty<PlayState> State { get; }
        
        BindableProperty<float> OxygenIntervalTime { get; }
        
        BindableProperty<WeaponTypes> CurrentWeaponType { get; }
        
        BindableProperty<bool> IfChestOpening { get; }
        
        BindableProperty<float> OpenChestSeconds { get; }
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

        public BindableProperty<float> OxygenIntervalTime { get; } =
            new BindableProperty<float>(Config.OxygenIntervalTime);

        public BindableProperty<WeaponTypes> CurrentWeaponType { get; } =
            new BindableProperty<WeaponTypes>(WeaponTypes.FishFork);

        public BindableProperty<bool> IfChestOpening { get; } = new BindableProperty<bool>(false);

        public BindableProperty<float> OpenChestSeconds { get; } = new BindableProperty<float>(0f);

        public BindableProperty<bool> Invincibility { get; } = new BindableProperty<bool>(false);

        public BindableProperty<bool> IfLeft { get; } = new BindableProperty<bool>(false);

        public BindableProperty<PlayState> State { get; } = new BindableProperty<PlayState>(PlayState.Swim);
    }
}