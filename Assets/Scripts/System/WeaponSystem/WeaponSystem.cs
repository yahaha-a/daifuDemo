using System;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public interface IWeaponSystem : ISystem
    {
        Dictionary<string, Dictionary<int, IGunInfo>> GunInfos { get; }
        
        Dictionary<string, Dictionary<int, IMeleeWeaponInfo>> MeleeWeaponInfos { get; }
        
        Dictionary<string, Dictionary<int, IFishForkInfo>> FishForkInfos { get; }

        IWeaponSystem AddGunInfo(string key, int rank, IGunInfo gunInfo);

        IWeaponSystem AddMeleeWeaponInfo(string key, int rank, IMeleeWeaponInfo meleeWeaponInfo);

        IWeaponSystem AddFishForkInfo(string key, int rank, IFishForkInfo fishForkInfo);
    }
    
    public class WeaponSystem : AbstractSystem, IWeaponSystem
    {
        private IBulletSystem _bulletSystem;

        public Dictionary<string, Dictionary<int, IGunInfo>> GunInfos { get; } = new Dictionary<string, Dictionary<int, IGunInfo>>();

        public Dictionary<string, Dictionary<int, IMeleeWeaponInfo>> MeleeWeaponInfos { get; } =
            new Dictionary<string, Dictionary<int, IMeleeWeaponInfo>>();

        public Dictionary<string, Dictionary<int, IFishForkInfo>> FishForkInfos { get; } =
            new Dictionary<string, Dictionary<int, IFishForkInfo>>();

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

            this.AddMeleeWeaponInfo(Config.DaggerKey, 1, new MeleeWeaponInfo()
                .WithName("匕首")
                .WithDamage(5f)
                .WithAttackRadius(10f)
                .WithAttackFrequency(0.5f));

            this.AddFishForkInfo(Config.FishForkKey, 1, new FishForkInfo()
                .WithName("鱼叉")
                .WithRotationRate(50f)
                .WithSpeed(30f)
                .WithFishForkLength(10f));
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

        public IWeaponSystem AddMeleeWeaponInfo(string key, int rank, IMeleeWeaponInfo meleeWeaponInfo)
        {
            MeleeWeaponInfos.Add(key, new Dictionary<int, IMeleeWeaponInfo>()
            {
                {
                    rank, meleeWeaponInfo
                }
            });

            return this;
        }

        public IWeaponSystem AddFishForkInfo(string key, int rank, IFishForkInfo fishForkInfo)
        {
            FishForkInfos.Add(key, new Dictionary<int, IFishForkInfo>()
            {
                {
                    rank, fishForkInfo
                }
            });

            return this;
        }
    }
}