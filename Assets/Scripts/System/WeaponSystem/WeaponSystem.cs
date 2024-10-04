using System;
using System.Collections.Generic;
using Global;
using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public interface IWeaponSystem : ISystem
    {
        Dictionary<(string, int), IGunInfo> GunInfos { get; }
        
        Dictionary<(string, int), IMeleeWeaponInfo>  MeleeWeaponInfos { get; }
        
        Dictionary<(string, int), IFishForkInfo> FishForkInfos { get; }
        
        IWeaponSystem AddGunInfo(string key, int rank, IGunInfo gunInfo);

        IWeaponSystem AddMeleeWeaponInfo(string key, int rank, IMeleeWeaponInfo meleeWeaponInfo);

        IWeaponSystem AddFishForkInfo(string key, int rank, IFishForkInfo fishForkInfo);
    }
    
    public class WeaponSystem : AbstractSystem, IWeaponSystem
    {
        private IBulletSystem _bulletSystem;

        public Dictionary<(string, int), IGunInfo> GunInfos { get; } = new Dictionary<(string, int), IGunInfo>();

        public Dictionary<(string, int), IMeleeWeaponInfo> MeleeWeaponInfos { get; } =
            new Dictionary<(string, int), IMeleeWeaponInfo>();

        public Dictionary<(string, int), IFishForkInfo> FishForkInfos { get; } =
            new Dictionary<(string, int), IFishForkInfo>();

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
                .AddGunInfo(Config.RifleKey, 2, new GunInfo()
                    .WithKey(Config.RifleKey)
                    .WithName("步枪")
                    .WithIntervalBetweenShots(0.18f)
                    .WithRotationRate(110f)
                    .WithBulletSpawnLocationsAndDirectionsList(new List<(Vector2, float)>()
                    {
                        (new Vector2(0, 0), 0f)
                    }))
                .AddGunInfo(Config.RifleKey, 3, new GunInfo()
                    .WithKey(Config.RifleKey)
                    .WithName("步枪")
                    .WithIntervalBetweenShots(0.16f)
                    .WithRotationRate(120f)
                    .WithBulletSpawnLocationsAndDirectionsList(new List<(Vector2, float)>()
                    {
                        (new Vector2(0, 0), 0f)
                    }))
                .AddGunInfo(Config.RifleKey, 4, new GunInfo()
                    .WithKey(Config.RifleKey)
                    .WithName("步枪")
                    .WithIntervalBetweenShots(0.14f)
                    .WithRotationRate(130f)
                    .WithBulletSpawnLocationsAndDirectionsList(new List<(Vector2, float)>()
                    {
                        (new Vector2(0, 0), 0f)
                    }))
                .AddGunInfo(Config.RifleKey, 5, new GunInfo()
                    .WithKey(Config.RifleKey)
                    .WithName("步枪")
                    .WithIntervalBetweenShots(0.12f)
                    .WithRotationRate(140f)
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
                    }))
                .AddGunInfo(Config.ShotgunKey, 2, new GunInfo()
                    .WithKey(Config.ShotgunKey)
                    .WithName("霰弹枪")
                    .WithIntervalBetweenShots(0.9f)
                    .WithRotationRate(130f)
                    .WithBulletSpawnLocationsAndDirectionsList(new List<(Vector2, float)>()
                    {
                        (new Vector2(0, 0), 0f),
                        (new Vector2(0, 0), 10f),
                        (new Vector2(0, 0), -10f)
                    }))
                .AddGunInfo(Config.ShotgunKey, 3, new GunInfo()
                    .WithKey(Config.ShotgunKey)
                    .WithName("霰弹枪")
                    .WithIntervalBetweenShots(0.8f)
                    .WithRotationRate(130f)
                    .WithBulletSpawnLocationsAndDirectionsList(new List<(Vector2, float)>()
                    {
                        (new Vector2(0, 0), 10f),
                        (new Vector2(0, 0), 5f),
                        (new Vector2(0, 0), 0f),
                        (new Vector2(0, 0), -5f),
                        (new Vector2(0, 0), -10f)
                    }))
                .AddGunInfo(Config.ShotgunKey, 4, new GunInfo()
                    .WithKey(Config.ShotgunKey)
                    .WithName("霰弹枪")
                    .WithIntervalBetweenShots(0.7f)
                    .WithRotationRate(140f)
                    .WithBulletSpawnLocationsAndDirectionsList(new List<(Vector2, float)>()
                    {
                        (new Vector2(0, 0), 10f),
                        (new Vector2(0, 0), 5f),
                        (new Vector2(0, 0), 0f),
                        (new Vector2(0, 0), -5f),
                        (new Vector2(0, 0), -10f)
                    }))
                .AddGunInfo(Config.ShotgunKey, 5, new GunInfo()
                    .WithKey(Config.ShotgunKey)
                    .WithName("霰弹枪")
                    .WithIntervalBetweenShots(0.5f)
                    .WithRotationRate(150f)
                    .WithBulletSpawnLocationsAndDirectionsList(new List<(Vector2, float)>()
                    {
                        (new Vector2(0, 0), 15f),
                        (new Vector2(0, 0), 10f),
                        (new Vector2(0, 0), 5f),
                        (new Vector2(0, 0), 0f),
                        (new Vector2(0, 0), -5f),
                        (new Vector2(0, 0), -10f),
                        (new Vector2(0, 0), -15f)
                    }));

            this.AddMeleeWeaponInfo(Config.DaggerKey, 1, new MeleeWeaponInfo()
                    .WithName("匕首")
                    .WithDamage(5f)
                    .WithAttackRadius(10f)
                    .WithAttackFrequency(0.5f))
                .AddMeleeWeaponInfo(Config.DaggerKey, 2, new MeleeWeaponInfo()
                    .WithName("匕首")
                    .WithDamage(6f)
                    .WithAttackRadius(11f)
                    .WithAttackFrequency(0.4f))
                .AddMeleeWeaponInfo(Config.DaggerKey, 3, new MeleeWeaponInfo()
                    .WithName("匕首")
                    .WithDamage(7f)
                    .WithAttackRadius(12f)
                    .WithAttackFrequency(0.4f))
                .AddMeleeWeaponInfo(Config.DaggerKey, 4, new MeleeWeaponInfo()
                    .WithName("匕首")
                    .WithDamage(8f)
                    .WithAttackRadius(13f)
                    .WithAttackFrequency(0.3f))
                .AddMeleeWeaponInfo(Config.DaggerKey, 5, new MeleeWeaponInfo()
                    .WithName("匕首")
                    .WithDamage(10f)
                    .WithAttackRadius(14f)
                    .WithAttackFrequency(0.3f));

            this.AddFishForkInfo(Config.FishForkKey, 1, new FishForkInfo()
                .WithName("鱼叉")
                .WithRotationRate(50f)
                .WithSpeed(30f)
                .WithFishForkLength(11f))
            .AddFishForkInfo(Config.FishForkKey, 2, new FishForkInfo()
                .WithName("鱼叉")
                .WithRotationRate(55f)
                .WithSpeed(32f)
                .WithFishForkLength(12f))
            .AddFishForkInfo(Config.FishForkKey, 3, new FishForkInfo()
                .WithName("鱼叉")
                .WithRotationRate(60f)
                .WithSpeed(34f)
                .WithFishForkLength(13f))
            .AddFishForkInfo(Config.FishForkKey, 4, new FishForkInfo()
                .WithName("鱼叉")
                .WithRotationRate(65f)
                .WithSpeed(36f)
                .WithFishForkLength(14f))
            .AddFishForkInfo(Config.FishForkKey, 5, new FishForkInfo()
                .WithName("鱼叉")
                .WithRotationRate(70f)
                .WithSpeed(38f)
                .WithFishForkLength(15f));
        }

        public IWeaponSystem AddGunInfo(string key, int rank, IGunInfo gunInfo)
        {
            GunInfos.Add((key, rank), gunInfo);
            
            return this;
        }

        public IWeaponSystem AddMeleeWeaponInfo(string key, int rank, IMeleeWeaponInfo meleeWeaponInfo)
        {
            MeleeWeaponInfos.Add((key, rank), meleeWeaponInfo);

            return this;
        }

        public IWeaponSystem AddFishForkInfo(string key, int rank, IFishForkInfo fishForkInfo)
        {
            FishForkInfos.Add((key, rank), fishForkInfo);

            return this;
        }
    }
}