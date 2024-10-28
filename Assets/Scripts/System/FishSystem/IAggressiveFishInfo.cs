using System;

namespace daifuDemo
{
    public interface IAggressiveFishInfo : IFishInfo
    {
        float Damage { get; }

        float PursuitSwimRate { get; }
        
        float AttackInterval { get; }
        
        float AttackRange { get; }
        
        float ChargeTime { get; }

        IAggressiveFishInfo WithDamage(float damage);

        IAggressiveFishInfo WithPursuitSwimRate(float pursuitSwimRate);

        IAggressiveFishInfo WithAttackInterval(float attackInterval);

        IAggressiveFishInfo WithAttackRange(float attackRange);

        IAggressiveFishInfo WithChargeTime(float chargeTime);
    }

    public class AggressiveFishInfo : FishInfo, IAggressiveFishInfo
    {
        public float Damage { get; private set; }
        
        public float PursuitSwimRate { get; private set; }
        
        public float AttackInterval { get; private set; }
        
        public float AttackRange { get; private set; }

        public float ChargeTime { get; private set; }

        public IAggressiveFishInfo WithDamage(float damage)
        {
            Damage = damage;
            return this;
        }
        
        public IAggressiveFishInfo WithPursuitSwimRate(float pursuitSwimRate)
        {
            PursuitSwimRate = pursuitSwimRate;
            return this;
        }

        public IAggressiveFishInfo WithAttackInterval(float attackInterval)
        {
            AttackInterval = attackInterval;
            return this;
        }

        public IAggressiveFishInfo WithAttackRange(float attackRange)
        {
            AttackRange = attackRange;
            return this;
        }

        public IAggressiveFishInfo WithChargeTime(float chargeTime)
        {
            ChargeTime = chargeTime;
            return this;
        }
    }
}