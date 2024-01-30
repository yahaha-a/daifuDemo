using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public interface IFish : IController
    {
        string FishKey { get; }

        FishState FishState { get; }
		
        float ToggleDirectionTime { get; }
        
        float RangeOfMovement { get; }

        float SwimRate { get; }
        
        float FrightenedSwimRate { get; }
        
        Vector2 CurrentDirection { get; }

        Vector3 StartPosition { get; }

        float CurrentSwimRate { get; }

        float CurrentToggleDirectionTime { get; }
        
        float Hp { get; set; }
    }

    public interface IAggressiveFish : IFish
    {
        float Damage { get; }
        
        float PursuitSwimRate { get; }
    }
}