using UnityEngine;

namespace daifuDemo
{
    public enum FishState
    {
        Swim,
        Frightened,
        Hit,
        Caught
    }
    
    public interface IFishInfo
    {
        string FishName { get; }
        
        string FishKey { get; }
        
        GameObject FishPrefab { get; }
        
        FishState FishState { get;}
        
        float ToggleDirectionTime { get; }
        
        float RangeOfMovement { get; }
        
        float SwimRate { get; }
        
        float FrightenedSwimRate { get; }
    }
    
    public class FishInfo : IFishInfo
    {
        public string FishName { get; private set; }
        
        public string FishKey { get; private set; }
        
        public GameObject FishPrefab { get; private set; }

        public FishState FishState { get; private set; }
        
        public float ToggleDirectionTime { get; private set; }
        
        public float RangeOfMovement { get; private set; }

        public float SwimRate { get; private set; }
        
        public float FrightenedSwimRate { get; private set; }

        public FishInfo WithFishName(string fishName)
        {
            FishName = fishName;
            return this;
        }

        public FishInfo WithFishKey(string fishKey)
        {
            FishKey = fishKey;
            return this;
        }

        public FishInfo WithFishPrefab(GameObject fishPrefab)
        {
            FishPrefab = fishPrefab;
            return this;
        }

        public FishInfo WithFishState(FishState fishState)
        {
            FishState = fishState;
            return this;
        }

        public FishInfo WithToggleDirectionTime(float toggleDirectionTime)
        {
            ToggleDirectionTime = toggleDirectionTime;
            return this;
        }

        public FishInfo WithRangeOfMovement(float rangeOfMovement)
        {
            RangeOfMovement = rangeOfMovement;
            return this;
        }

        public FishInfo WithSwimRate(float swimRate)
        {
            SwimRate = swimRate;
            return this;
        }

        public FishInfo WithFrightenedSwimRate(float frightenedSwimRate)
        {
            FrightenedSwimRate = frightenedSwimRate;
            return this;
        }
    }
}