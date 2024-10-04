using Global;
using QFramework;
using UnityEngine;

namespace daifuDemo
{
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

        BindableProperty<float> OxygenIntervalTime { get; }
        
        BindableProperty<WeaponTypes> CurrentWeaponType { get; }
        
        BindableProperty<bool> IfChestOpening { get; }
        
        BindableProperty<float> OpenChestSeconds { get; }
        
        BindableProperty<int> MaxFishingChallengeClicks { get; }
        
        BindableProperty<int> FishingChallengeClicks { get; }
        
        BindableProperty<bool> IfCatchingFish { get; }
        
        BindableProperty<bool> IfCanPickUp { get; }
        
        BindableProperty<bool> IfCanOpenTreasureChests { get; }
        
        BindableProperty<PickUpItem> CurrentPickUpItem { get; }
        
        BindableProperty<PlayState> CurrentState { get; }
        
        BindableProperty<bool> IfAttacking { get; }
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

        public BindableProperty<int> MaxFishingChallengeClicks { get; } = new BindableProperty<int>(0);

        public BindableProperty<int> FishingChallengeClicks { get; } = new BindableProperty<int>(0);
        
        public BindableProperty<bool> IfCatchingFish { get; } = new BindableProperty<bool>(false);
        
        public BindableProperty<bool> IfCanPickUp { get; } = new BindableProperty<bool>(false);
        
        public BindableProperty<bool> IfCanOpenTreasureChests { get; } = new BindableProperty<bool>(false);
        
        public BindableProperty<PickUpItem> CurrentPickUpItem { get; } = new BindableProperty<PickUpItem>(null);
        
        public BindableProperty<PlayState> CurrentState { get; } = new BindableProperty<PlayState>(PlayState.Swim);
        
        public BindableProperty<bool> IfAttacking { get; } = new BindableProperty<bool>(false);

        public BindableProperty<bool> Invincibility { get; } = new BindableProperty<bool>(false);

        public BindableProperty<bool> IfLeft { get; } = new BindableProperty<bool>(false);
    }
}