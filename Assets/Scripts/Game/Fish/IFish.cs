using QFramework;

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
    }
}