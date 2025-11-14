using System.Collections.Generic;
using UnityEngine;

namespace daifuDemo
{
    public enum WeaponType
    {
        FishFork,
        Gun,
        MeleeWeapon
    }
    
    public interface IGunInfo
    {
        int MaximumAmmunition { get; }
        
        float LoadAmmunitionNeedTime { get; }
        
        float RateOfFire { get; }
        
        float IntervalBetweenShots { get; }
        
        float AttackRange { get; }
        
        IndicatorType IndicatorType { get; }
        
        List<(Vector2, float)> BulletSpawnLocationsAndDirectionsList { get; }
        
        IGunInfo WithMaximumAmmunition(int maximumAmmunition);

        IGunInfo WithLoadAmmunitionNeedTime(float loadAmmunitionNeedTime);

        IGunInfo WithRateOfFire(float rateOfFire);
        
        IGunInfo WithIntervalBetweenShots(float intervalBetweenShots);

        IGunInfo WithAttackRange(float attackRange);
        
        IGunInfo WithIndicatorType(IndicatorType indicatorType);
        
        IGunInfo WithBulletSpawnLocationsAndDirectionsList(
            List<(Vector2, float)> bulletSpawnLocationsAndDirectionsList);
    }
    
    public class GunInfo : WeaponInfo<GunInfo>, IGunInfo
    {
        public int MaximumAmmunition { get; private set; }
        public float LoadAmmunitionNeedTime { get; private set; }
        public float RateOfFire { get; private set; }
        public float IntervalBetweenShots { get; private set; }
        public float AttackRange { get; private set; }
        public IndicatorType IndicatorType { get; private set; }
        public List<(Vector2, float)> BulletSpawnLocationsAndDirectionsList { get; private set; }

        public GunInfo WithMaximumAmmunition(int maximumAmmunition)
        {
            MaximumAmmunition = maximumAmmunition;
            return this;
        }

        public GunInfo WithLoadAmmunitionNeedTime(float loadAmmunitionNeedTime)
        {
            LoadAmmunitionNeedTime = loadAmmunitionNeedTime;
            return this;
        }

        public GunInfo WithRateOfFire(float rateOfFire)
        {
            RateOfFire = rateOfFire;
            return this;
        }

        public GunInfo WithIntervalBetweenShots(float intervalBetweenShots)
        {
            IntervalBetweenShots = intervalBetweenShots;
            return this;
        }

        public GunInfo WithAttackRange(float attackRange)
        {
            AttackRange = attackRange;
            return this;
        }

        public GunInfo WithIndicatorType(IndicatorType indicatorType)
        {
            IndicatorType = indicatorType;
            return this;
        }

        public GunInfo WithBulletSpawnLocationsAndDirectionsList(List<(Vector2, float)> bulletSpawnLocationsAndDirectionsList)
        {
            BulletSpawnLocationsAndDirectionsList = bulletSpawnLocationsAndDirectionsList;
            return this;
        }

        IGunInfo IGunInfo.WithMaximumAmmunition(int maximumAmmunition) => WithMaximumAmmunition(maximumAmmunition);
        IGunInfo IGunInfo.WithLoadAmmunitionNeedTime(float loadAmmunitionNeedTime) => WithLoadAmmunitionNeedTime(loadAmmunitionNeedTime);
        IGunInfo IGunInfo.WithRateOfFire(float rateOfFire) => WithRateOfFire(rateOfFire);
        IGunInfo IGunInfo.WithIntervalBetweenShots(float intervalBetweenShots) => WithIntervalBetweenShots(intervalBetweenShots);
        IGunInfo IGunInfo.WithAttackRange(float attackRange) => WithAttackRange(attackRange);
        IGunInfo IGunInfo.WithIndicatorType(IndicatorType indicatorType) => WithIndicatorType(indicatorType);
        IGunInfo IGunInfo.WithBulletSpawnLocationsAndDirectionsList(List<(Vector2, float)> bulletSpawnLocationsAndDirectionsList) 
            => WithBulletSpawnLocationsAndDirectionsList(bulletSpawnLocationsAndDirectionsList);
    }
}
