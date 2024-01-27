using QFramework;

namespace daifuDemo
{
    public interface IFish : IController
    {
        FishState FishState { get; }
		
        float ToggleDirectionTime { get; }

        float SwimRate { get; }
    }
}