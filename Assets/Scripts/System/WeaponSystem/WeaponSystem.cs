using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public interface IWeaponSystem : ISystem
    {
        Dictionary<string, Dictionary<int, IGunInfo>> GunInfos { get; }

        IWeaponSystem AddGunInfo(string key, int rank, IGunInfo gunInfo);
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
                    .WithRotationRate(100f)
                    .WithBulletSpawnLocationsAndDirectionsList(new List<(Vector2, float)>()
                    {
                        (new Vector2(0, 0), 0f)
                    }))
                .AddGunInfo(Config.ShotgunKey, 1, new GunInfo()
                    .WithKey(Config.ShotgunKey)
                    .WithName("霰弹枪")
                    .WithIntervalBetweenShots(1f)
                    .WithRotationRate(120f)
                    .WithBulletSpawnLocationsAndDirectionsList(new List<(Vector2, float)>()
                    {
                        (new Vector2(0, 0), 0f),
                        (new Vector2(0, 0), 10f),
                        (new Vector2(0, 0), -10f)
                    }));
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