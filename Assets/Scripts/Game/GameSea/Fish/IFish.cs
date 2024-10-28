using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public interface IFish : IController
    {
        string FishKey { get; set; }

        float ToggleDirectionTime { get; set; }
        
        float RangeOfMovement { get; set; }

        float SwimRate { get; set; }
        
        float FrightenedSwimRate { get; set; }
        
        float CoolDownTime { get; set; }
        
        float CurrentCoolDownTime { get; set; }
        
        Vector2 CurrentDirection { get; set; }
        
        Vector2 TargetDirection { get; set; }

        Vector3 StartPosition { get; set; }

        float CurrentSwimRate { get; set; }

        float CurrentToggleDirectionTime { get; set; }
        
        float Hp { get; set; }
        
        float FleeHp { get; set; }
        
        float StruggleTime { get; set; }
        
        float CurrentStruggleTime { get; set; }
        
        bool IfStruggle { get; set; }
        
        float VisualField { get; set; }
        
        int Clicks { get; set; }
        
        bool CanSwim { get; set; }
        
        bool HitByFork { get; set; }
        
        bool DiscoverPlayer { get; set; }
        
        bool HitByBullet { get; set; }
        
        Vector2 HitPosition { get; set; }
    }

    public interface IAggressiveFish : IFish
    {
        float Damage { get; set; }
        
        float PursuitSwimRate { get; set; }
        
        float AttackInterval { get; set; }
        
        float CurrentAttackInterval { get; set; }
        
        Vector2 CurrentAttackTargetPosition { get; set; }
        
        float AttackRange { get; set; }
        
        bool IfAttack { get; set; }
        
        float ChargeTime { get; set; }
        
        bool IfCharge { get; set; }
        
        float CurrentChargeTime { get; set; }
    }
}