using UnityEngine;

namespace daifuDemo
{
    public enum FishState
    {
        Swim,
        Frightened,
        Attack,
        Hit,
        Caught,
        Dead
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
        
        float Hp { get; }
        
        IFishInfo WithFishName(string fishName);

        IFishInfo WithFishKey(string fishKey);

        IFishInfo WithFishPrefab(GameObject fishPrefab);

        IFishInfo WithFishState(FishState fishState);

        IFishInfo WithToggleDirectionTime(float toggleDirectionTime);

        IFishInfo WithRangeOfMovement(float rangeOfMovement);

        IFishInfo WithSwimRate(float swimRate);

        IFishInfo WithFrightenedSwimRate(float frightenedSwimRate);

        IFishInfo WithHp(float hp);
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
        
        public float Hp { get; private set; }

        public IFishInfo WithFishName(string fishName)
        {
            FishName = fishName;
            return this;
        }

        public IFishInfo WithFishKey(string fishKey)
        {
            FishKey = fishKey;
            return this;
        }

        public IFishInfo WithFishPrefab(GameObject fishPrefab)
        {
            FishPrefab = fishPrefab;
            return this;
        }

        public IFishInfo WithFishState(FishState fishState)
        {
            FishState = fishState;
            return this;
        }

        public IFishInfo WithToggleDirectionTime(float toggleDirectionTime)
        {
            ToggleDirectionTime = toggleDirectionTime;
            return this;
        }

        public IFishInfo WithRangeOfMovement(float rangeOfMovement)
        {
            RangeOfMovement = rangeOfMovement;
            return this;
        }

        public IFishInfo WithSwimRate(float swimRate)
        {
            SwimRate = swimRate;
            return this;
        }

        public IFishInfo WithFrightenedSwimRate(float frightenedSwimRate)
        {
            FrightenedSwimRate = frightenedSwimRate;
            return this;
        }

        public IFishInfo WithHp(float hp)
        {
            Hp = hp;
            return this;
        }
    }
}