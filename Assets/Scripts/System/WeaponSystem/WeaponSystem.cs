using System;
using System.Collections.Generic;
using Global;
using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public interface IWeaponSystem : ISystem
    {
        Dictionary<(string, int), IWeaponInfo> WeaponInfos { get; }
        
        Dictionary<string, int> WeaponOwnInfos { get; }

        IWeaponSystem AddWeaponInfos(string key, int rank, IWeaponInfo weaponInfo);

        List<IWeaponInfo> FindObtainWeaponInfos(EquipWeaponKey weaponKey);

        void UpdateEquipWeapon(EquipWeaponKey weaponKey,
            BindableProperty<IWeaponItemTempleteInfo> weaponItemTempleteInfo);
    }
    
    public class WeaponSystem : AbstractSystem, IWeaponSystem
    {
        private IUIGameShipPanelModel _uiGameShipPanelModel;
        private IBulletSystem _bulletSystem;
        private IArchiveSystem _archiveSystem;

        public Dictionary<(string, int), IWeaponInfo> WeaponInfos { get; } = new Dictionary<(string, int), IWeaponInfo>();

        public Dictionary<string, int> WeaponOwnInfos { get; } = new Dictionary<string, int>()
        {
            { Config.FishForkKey, 1 },
            {Config.DaggerKey, 1},
            { Config.RifleKey, 1 },
            { Config.ShotgunKey, 1 }
        };


        protected override void OnInit()
        {
            _uiGameShipPanelModel = this.GetModel<IUIGameShipPanelModel>();
            _bulletSystem = this.GetSystem<IBulletSystem>();
            _archiveSystem = this.GetSystem<IArchiveSystem>();
            
            Events.GameStart.Register(() =>
            {
                _archiveSystem.LoadData(WeaponOwnInfos, "WeaponOwnInfos");
            });

            //TODO
            this.AddWeaponInfos(Config.RifleKey, 1, new GunInfo()
                    .WithKey(Config.RifleKey)
                    .WithType(WeaponType.Gun)
                    .WithRank(1)
                    .WithName("步枪")
                    .WithIcon(null)
                    .WithRateOfFire(6f)
                    .WithAttackRange(6f)
                    .WithIntervalBetweenShots(1f)
                    .WithMaximumAmmunition(15)
                    .WithLoadAmmunitionNeedTime(3f)
                    .WithBulletSpawnLocationsAndDirectionsList(new List<(Vector2, float)>()
                    {
                        (new Vector2(0, 0), 0f)
                    }))
                
                .AddWeaponInfos(Config.RifleKey, 2, new GunInfo()
                    .WithKey(Config.RifleKey)
                    .WithType(WeaponType.Gun)
                    .WithRank(2)
                    .WithName("步枪")
                    .WithIcon(null)
                    .WithRateOfFire(10f)
                    .WithAttackRange(6f)
                    .WithIntervalBetweenShots(1f)
                    .WithMaximumAmmunition(15)
                    .WithLoadAmmunitionNeedTime(3f)
                    .WithBulletSpawnLocationsAndDirectionsList(new List<(Vector2, float)>()
                    {
                        (new Vector2(0, 0), 0f)
                    }))
                
                .AddWeaponInfos(Config.RifleKey, 3, new GunInfo()
                    .WithKey(Config.RifleKey)
                    .WithType(WeaponType.Gun)
                    .WithRank(3)
                    .WithName("步枪")
                    .WithIcon(null)
                    .WithRateOfFire(10f)
                    .WithAttackRange(10f)
                    .WithIntervalBetweenShots(1f)
                    .WithMaximumAmmunition(15)
                    .WithLoadAmmunitionNeedTime(3f)
                    .WithBulletSpawnLocationsAndDirectionsList(new List<(Vector2, float)>()
                    {
                        (new Vector2(0, 0), 0f)
                    }))
                
                .AddWeaponInfos(Config.RifleKey, 4, new GunInfo()
                    .WithKey(Config.RifleKey)
                    .WithType(WeaponType.Gun)
                    .WithRank(4)
                    .WithName("步枪")
                    .WithIcon(null)
                    .WithRateOfFire(10f)
                    .WithAttackRange(10f)
                    .WithIntervalBetweenShots(1f)
                    .WithMaximumAmmunition(15)
                    .WithLoadAmmunitionNeedTime(3f)
                    .WithBulletSpawnLocationsAndDirectionsList(new List<(Vector2, float)>()
                    {
                        (new Vector2(0, 0), 0f)
                    }))
                
                .AddWeaponInfos(Config.RifleKey, 5, new GunInfo()
                    .WithKey(Config.RifleKey)
                    .WithType(WeaponType.Gun)
                    .WithRank(5)
                    .WithName("步枪")
                    .WithIcon(null)
                    .WithRateOfFire(10f)
                    .WithAttackRange(10f)
                    .WithIntervalBetweenShots(0.5f)
                    .WithMaximumAmmunition(15)
                    .WithLoadAmmunitionNeedTime(3f)
                    .WithBulletSpawnLocationsAndDirectionsList(new List<(Vector2, float)>()
                    {
                        (new Vector2(0, 0), 0f)
                    }))
                
                .AddWeaponInfos(Config.RifleKey, 6, new GunInfo()
                    .WithKey(Config.RifleKey)
                    .WithType(WeaponType.Gun)
                    .WithRank(6)
                    .WithName("步枪")
                    .WithIcon(null)
                    .WithRateOfFire(10f)
                    .WithAttackRange(10f)
                    .WithIntervalBetweenShots(0.5f)
                    .WithMaximumAmmunition(30)
                    .WithLoadAmmunitionNeedTime(3f)
                    .WithBulletSpawnLocationsAndDirectionsList(new List<(Vector2, float)>()
                    {
                        (new Vector2(0, 0), 0f)
                    }))
               
                
                
                .AddWeaponInfos(Config.ShotgunKey, 1, new GunInfo()
                    .WithKey(Config.ShotgunKey)
                    .WithType(WeaponType.Gun)
                    .WithRank(1)
                    .WithName("霰弹枪")
                    .WithIcon(null)
                    .WithRateOfFire(4f)
                    .WithAttackRange(4f)
                    .WithIntervalBetweenShots(2f)
                    .WithMaximumAmmunition(6)
                    .WithLoadAmmunitionNeedTime(5f)
                    .WithBulletSpawnLocationsAndDirectionsList(new List<(Vector2, float)>()
                    {
                        (new Vector2(0, 0), 0f),
                        (new Vector2(0, 0), 10f),
                        (new Vector2(0, 0), -10f)
                    }))
                
                .AddWeaponInfos(Config.ShotgunKey, 2, new GunInfo()
                    .WithKey(Config.ShotgunKey)
                    .WithType(WeaponType.Gun)
                    .WithRank(2)
                    .WithName("霰弹枪")
                    .WithIcon(null)
                    .WithRateOfFire(8f)
                    .WithAttackRange(4f)
                    .WithIntervalBetweenShots(2f)
                    .WithMaximumAmmunition(6)
                    .WithLoadAmmunitionNeedTime(5f)
                    .WithBulletSpawnLocationsAndDirectionsList(new List<(Vector2, float)>()
                    {
                        (new Vector2(0, 0), 0f),
                        (new Vector2(0, 0), 10f),
                        (new Vector2(0, 0), -10f)
                    }))
                
                .AddWeaponInfos(Config.ShotgunKey, 3, new GunInfo()
                    .WithKey(Config.ShotgunKey)
                    .WithType(WeaponType.Gun)
                    .WithRank(3)
                    .WithName("霰弹枪")
                    .WithIcon(null)
                    .WithRateOfFire(8f)
                    .WithAttackRange(6f)
                    .WithIntervalBetweenShots(2f)
                    .WithMaximumAmmunition(6)
                    .WithLoadAmmunitionNeedTime(5f)
                    .WithBulletSpawnLocationsAndDirectionsList(new List<(Vector2, float)>()
                    {
                        (new Vector2(0, 0), 0f),
                        (new Vector2(0, 0), 10f),
                        (new Vector2(0, 0), -10f)
                    }))
                
                .AddWeaponInfos(Config.ShotgunKey, 4, new GunInfo()
                    .WithKey(Config.ShotgunKey)
                    .WithType(WeaponType.Gun)
                    .WithRank(4)
                    .WithName("霰弹枪")
                    .WithIcon(null)
                    .WithRateOfFire(8f)
                    .WithAttackRange(6f)
                    .WithIntervalBetweenShots(1.5f)
                    .WithMaximumAmmunition(6)
                    .WithLoadAmmunitionNeedTime(5f)
                    .WithBulletSpawnLocationsAndDirectionsList(new List<(Vector2, float)>()
                    {
                        (new Vector2(0, 0), 0f),
                        (new Vector2(0, 0), 10f),
                        (new Vector2(0, 0), -10f)
                    }))
                
                .AddWeaponInfos(Config.ShotgunKey, 5, new GunInfo()
                    .WithKey(Config.ShotgunKey)
                    .WithType(WeaponType.Gun)
                    .WithRank(5)
                    .WithName("霰弹枪")
                    .WithIcon(null)
                    .WithRateOfFire(8f)
                    .WithAttackRange(6f)
                    .WithIntervalBetweenShots(1.5f)
                    .WithMaximumAmmunition(9)
                    .WithLoadAmmunitionNeedTime(5f)
                    .WithBulletSpawnLocationsAndDirectionsList(new List<(Vector2, float)>()
                    {
                        (new Vector2(0, 0), 0f),
                        (new Vector2(0, 0), 10f),
                        (new Vector2(0, 0), -10f)
                    }))
                
                .AddWeaponInfos(Config.ShotgunKey, 6, new GunInfo()
                    .WithKey(Config.ShotgunKey)
                    .WithType(WeaponType.Gun)
                    .WithRank(6)
                    .WithName("霰弹枪")
                    .WithIcon(null)
                    .WithRateOfFire(8f)
                    .WithAttackRange(6f)
                    .WithIntervalBetweenShots(1.5f)
                    .WithMaximumAmmunition(15)
                    .WithLoadAmmunitionNeedTime(5f)
                    .WithBulletSpawnLocationsAndDirectionsList(new List<(Vector2, float)>()
                    {
                        (new Vector2(0, 0), 10f),
                        (new Vector2(0, 0), 5f),
                        (new Vector2(0, 0), 0f),
                        (new Vector2(0, 0), -5f),
                        (new Vector2(0, 0), -10f),
                    }));

            this.AddWeaponInfos(Config.DaggerKey, 1, new MeleeWeaponInfo()
                    .WithKey(Config.DaggerKey)
                    .WithName("匕首")
                    .WithRank(1)
                    .WithType(WeaponType.MeleeWeapon)
                    .WithDamage(5f)
                    .WithAttackRadius(10f)
                    .WithAttackFrequency(0.5f))
                .AddWeaponInfos(Config.DaggerKey, 2, new MeleeWeaponInfo()
                    .WithKey(Config.DaggerKey)
                    .WithName("匕首")
                    .WithRank(2)
                    .WithType(WeaponType.MeleeWeapon)
                    .WithDamage(6f)
                    .WithAttackRadius(11f)
                    .WithAttackFrequency(0.4f))
                .AddWeaponInfos(Config.DaggerKey, 3, new MeleeWeaponInfo()
                    .WithKey(Config.DaggerKey)
                    .WithName("匕首")
                    .WithRank(3)
                    .WithType(WeaponType.MeleeWeapon)
                    .WithDamage(7f)
                    .WithAttackRadius(12f)
                    .WithAttackFrequency(0.4f))
                .AddWeaponInfos(Config.DaggerKey, 4, new MeleeWeaponInfo()
                    .WithKey(Config.DaggerKey)
                    .WithName("匕首")
                    .WithRank(4)
                    .WithType(WeaponType.MeleeWeapon)
                    .WithDamage(8f)
                    .WithAttackRadius(13f)
                    .WithAttackFrequency(0.3f))
                .AddWeaponInfos(Config.DaggerKey, 5, new MeleeWeaponInfo()
                    .WithKey(Config.DaggerKey)
                    .WithName("匕首")
                    .WithRank(5)
                    .WithType(WeaponType.MeleeWeapon)
                    .WithDamage(10f)
                    .WithAttackRadius(14f)
                    .WithAttackFrequency(0.3f));

            this.AddWeaponInfos(Config.FishForkKey, 1, new FishForkInfo()
                    .WithKey(Config.FishForkKey)
                    .WithName("普通鱼叉")
                    .WithRank(1)
                    .WithType(WeaponType.FishFork)
                    .WithRotationRate(50f)
                    .WithSpeed(30f)
                    .WithFishForkLength(11f))
                .AddWeaponInfos(Config.FishForkKey, 2, new FishForkInfo()
                    .WithKey(Config.FishForkKey)
                    .WithName("普通鱼叉")
                    .WithRank(2)
                    .WithType(WeaponType.FishFork)
                    .WithRotationRate(55f)
                    .WithSpeed(32f)
                    .WithFishForkLength(12f))
                .AddWeaponInfos(Config.FishForkKey, 3, new FishForkInfo()
                    .WithKey(Config.FishForkKey)
                    .WithName("普通鱼叉")
                    .WithRank(3)
                    .WithType(WeaponType.FishFork)
                    .WithRotationRate(60f)
                    .WithSpeed(34f)
                    .WithFishForkLength(13f))
                .AddWeaponInfos(Config.FishForkKey, 4, new FishForkInfo()
                    .WithKey(Config.FishForkKey)
                    .WithName("普通鱼叉")
                    .WithRank(4)
                    .WithType(WeaponType.FishFork)
                    .WithRotationRate(65f)
                    .WithSpeed(36f)
                    .WithFishForkLength(14f))
                .AddWeaponInfos(Config.FishForkKey, 5, new FishForkInfo()
                    .WithKey(Config.FishForkKey)
                    .WithName("普通鱼叉")
                    .WithRank(5)
                    .WithType(WeaponType.FishFork)
                    .WithRotationRate(70f)
                    .WithSpeed(38f)
                    .WithFishForkLength(15f));
        }

        public IWeaponSystem AddWeaponInfos(string key, int rank, IWeaponInfo weaponInfo)
        {
            WeaponInfos.Add((key, rank), weaponInfo);
            return this;
        }

        public List<IWeaponInfo> FindObtainWeaponInfos(EquipWeaponKey weaponKey)
        {
            List<IWeaponInfo> weaponInfos = new List<IWeaponInfo>();
            
            if (weaponKey == EquipWeaponKey.Null)
            {
                return null;
            }
            
            if (weaponKey == EquipWeaponKey.FishFork)
            {
                foreach (var (key, rank) in WeaponOwnInfos)
                {
                    if (WeaponInfos[(key, rank)] != null && WeaponInfos[(key, rank)].Type == WeaponType.FishFork)
                    {
                        weaponInfos.Add(WeaponInfos[(key,rank)]);
                    }
                }

                return weaponInfos;
            }
            
            if (weaponKey == EquipWeaponKey.MeleeWeapon)
            {
                foreach (var (key, rank) in WeaponOwnInfos)
                {
                    if (WeaponInfos[(key, rank)] != null && WeaponInfos[(key, rank)].Type == WeaponType.MeleeWeapon)
                    {
                        weaponInfos.Add(WeaponInfos[(key,rank)]);
                    }
                }
                
                return weaponInfos;
            }
            
            if (weaponKey == EquipWeaponKey.PrimaryWeapon)
            {
                foreach (var (key, rank) in WeaponOwnInfos)
                {
                    if (WeaponInfos[(key, rank)] != null && WeaponInfos[(key, rank)].Type == WeaponType.Gun)
                    {
                        weaponInfos.Add(WeaponInfos[(key,rank)]);
                    }
                }
                
                return weaponInfos;
            }
            
            if (weaponKey == EquipWeaponKey.SecondaryWeapons)
            {
                foreach (var (key, rank) in WeaponOwnInfos)
                {
                    if (WeaponInfos[(key, rank)] != null && WeaponInfos[(key, rank)].Type == WeaponType.Gun)
                    {
                        weaponInfos.Add(WeaponInfos[(key,rank)]);
                    }
                }
                
                return weaponInfos;
            }
            
            return null;
        }

        public void UpdateEquipWeapon(EquipWeaponKey weaponKey,
            BindableProperty<IWeaponItemTempleteInfo> weaponItemTempleteInfo)
        {
            if (_uiGameShipPanelModel.CurrentEquipWeaponKey.Value == weaponKey)
            {
                if (weaponItemTempleteInfo.Value == null)
                {
                    if (_uiGameShipPanelModel.CurrentSelectWeaponInfo.Value.EquipState.Value == EquipWeaponKey.Null)
                    {
                        _uiGameShipPanelModel.CurrentSelectWeaponInfo.Value.WithEquipState(weaponKey);
                    }
                }
                else
                {
                    if (_uiGameShipPanelModel.CurrentSelectWeaponInfo.Value.EquipState.Value == weaponKey)
                    {
                        _uiGameShipPanelModel.CurrentSelectWeaponInfo.Value.WithEquipState(EquipWeaponKey.Null);
                    }
                }
            }
        }
    }
}