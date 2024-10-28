using System;
using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public interface IMeleeWeaponInfo
    {
        float Damage { get; }
        
        float AttackRadius { get; }
        
        float AttackFrequency { get; }
        
        IMeleeWeaponInfo WithDamage(float damage);

        IMeleeWeaponInfo WithAttackRadius(float attackRadius);

        IMeleeWeaponInfo WithAttackFrequency(float attackFrequency);
    }
    
    public class MeleeWeaponInfo : WeaponInfo<MeleeWeaponInfo>, IMeleeWeaponInfo
    {
        public float Damage { get; private set; }
        
        public float AttackRadius { get; private set; }
        
        public float AttackFrequency { get; private set; }

        public MeleeWeaponInfo WithDamage(float damage)
        {
            Damage = damage;
            return this;
        }

        public MeleeWeaponInfo WithAttackRadius(float attackRadius)
        {
            AttackRadius = attackRadius;
            return this;
        }

        public MeleeWeaponInfo WithAttackFrequency(float attackFrequency)
        {
            AttackFrequency = attackFrequency;
            return this;
        }

        IMeleeWeaponInfo IMeleeWeaponInfo.WithDamage(float damage) => WithDamage(damage);
        IMeleeWeaponInfo IMeleeWeaponInfo.WithAttackRadius(float attackRadius) => WithAttackRadius(attackRadius);
        IMeleeWeaponInfo IMeleeWeaponInfo.WithAttackFrequency(float attackFrequency) => WithAttackFrequency(attackFrequency);
    }
}