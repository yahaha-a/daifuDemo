using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public interface IFish : IController
    {
        string FishKey { get; set; }

        FishState FishState { get; set; }
		
        float ToggleDirectionTime { get; set; }
        
        float RangeOfMovement { get; set; }

        float SwimRate { get; set; }
        
        float FrightenedSwimRate { get; set; }
        
        Vector2 CurrentDirection { get; set; }

        Vector3 StartPosition { get; set; }

        float CurrentSwimRate { get; set; }

        float CurrentToggleDirectionTime { get; set; }
        
        float Hp { get; set; }

        void HitByFishFork();
    }

    public interface IAggressiveFish : IFish
    {
        float Damage { get; set; }
        
        float PursuitSwimRate { get; set; }
        
        float AttackInterval { get; set; }
    }
}