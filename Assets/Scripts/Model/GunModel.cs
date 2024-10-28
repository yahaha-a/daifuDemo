using System.Collections.Generic;
using Global;
using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public interface IGunModel : IModel
    {
        BindableProperty<string> CurrentGunKey { get; }
        
        BindableProperty<int> CurrentRank { get; }
        
        BindableProperty<GunState> CurrentGunState { get; }
        
        BindableProperty<bool> IfLeft { get; }

        BindableProperty<string> GunName { get; }

        BindableProperty<Texture2D> Icon { get; }

        BindableProperty<float> RateOfFire { get; }

        BindableProperty<float> AttackRange { get; }
        
        BindableProperty<int> MaximumAmmunition { get; }
        
        BindableProperty<float> IntervalBetweenShots { get; }
        
        BindableProperty<float> LoadAmmunitionNeedTime { get; }

        BindableProperty<List<(Vector2, float)>> BulletSpawnLocationsAndDirectionsList { get; }
        
        BindableProperty<float> CurrentIntervalBetweenShots { get; }

        BindableProperty<float> CurrentLoadAmmunitionTime { get; }

        BindableProperty<int> CurrentAllAmmunition { get; }

        BindableProperty<int> CurrentAmmunition { get; }
    }
    
    public class GunModel : AbstractModel, IGunModel
    {
        protected override void OnInit()
        {
            
        }

        public BindableProperty<string> CurrentGunKey { get; } = new BindableProperty<string>(Config.RifleKey);

        public BindableProperty<int> CurrentRank { get; } = new BindableProperty<int>(1);
        
        public BindableProperty<GunState> CurrentGunState { get; } = new BindableProperty<GunState>(GunState.Ready);
        
        public BindableProperty<bool> IfLeft { get; } = new BindableProperty<bool>(false);

        public BindableProperty<string> GunName { get; } = new BindableProperty<string>(null);
        
        public BindableProperty<Texture2D> Icon { get; } = new BindableProperty<Texture2D>(null);
        
        public BindableProperty<float> RateOfFire { get; } = new BindableProperty<float>(0);
        
        public BindableProperty<float> AttackRange { get; } = new BindableProperty<float>(0);
        
        public BindableProperty<int> MaximumAmmunition { get; } = new BindableProperty<int>(0);
        
        public BindableProperty<float> IntervalBetweenShots { get; } = new BindableProperty<float>(0);
        
        public BindableProperty<float> LoadAmmunitionNeedTime { get; } = new BindableProperty<float>(0);

        public BindableProperty<List<(Vector2, float)>> BulletSpawnLocationsAndDirectionsList { get; } =
            new BindableProperty<List<(Vector2, float)>>(null);

        public BindableProperty<float> CurrentIntervalBetweenShots { get; } = new BindableProperty<float>(0);
        
        public BindableProperty<float> CurrentLoadAmmunitionTime { get; } = new BindableProperty<float>(0);
        
        public BindableProperty<int> CurrentAllAmmunition { get; } = new BindableProperty<int>(0);
        
        public BindableProperty<int> CurrentAmmunition { get; } = new BindableProperty<int>(0);
    }
}