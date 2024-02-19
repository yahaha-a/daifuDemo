using System;
using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public interface IMeleeWeaponInfo
    {
        string Name { get; }
        
        float Damage { get; }
        
        float AttackRadius { get; }
        
        float AttackFrequency { get; }
        
        IMeleeWeaponInfo WithName(string name);
        
        IMeleeWeaponInfo WithDamage(float damage);

        IMeleeWeaponInfo WithAttackRadius(float attackRadius);

        IMeleeWeaponInfo WithAttackFrequency(float attackFrequency);
    }
    
    public class MeleeWeaponInfo : IMeleeWeaponInfo
    {
        public string Name { get; private set; }
        
        public float Damage { get; private set; }
        
        public float AttackRadius { get; private set; }
        
        public float AttackFrequency { get; private set; }
        
        public IMeleeWeaponInfo WithName(string name)
        {
            Name = name;
            return this;
        }

        public IMeleeWeaponInfo WithDamage(float damage)
        {
            Damage = damage;
            return this;
        }

        public IMeleeWeaponInfo WithAttackRadius(float attackRadius)
        {
            AttackRadius = attackRadius;
            return this;
        }

        public IMeleeWeaponInfo WithAttackFrequency(float attackFrequency)
        {
            AttackFrequency = attackFrequency;
            return this;
        }
    }
}