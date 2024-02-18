using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public interface IWeaponSystem : ISystem
    {
        Dictionary<string, Dictionary<int, IGunInfo>> GunInfos { get; }
    }
    
    public class WeaponSystem : AbstractSystem, IWeaponSystem
    {
        private IBulletSystem _bulletSystem;

        public Dictionary<string, Dictionary<int, IGunInfo>> GunInfos { get; } = new Dictionary<string, Dictionary<int, IGunInfo>>();
        
        protected override void OnInit()
        {
            _bulletSystem = this.GetSystem<IBulletSystem>();

            this.AddGunInfo(Config.RifleKey, 1, new GunInfo()
                .WithKey(Config.RifleKey)
                .WithName("步枪")
                .WithIntervalBetweenShots(0.2f)
                .WithRotationRate(100f));
        }

        public IWeaponSystem AddGunInfo(string key, int rank, IGunInfo gunInfo)
        {
            GunInfos.Add(key, new Dictionary<int, IGunInfo>()
            {
                {
                    rank, gunInfo
                }
            });
            return this;
        }
    }
}