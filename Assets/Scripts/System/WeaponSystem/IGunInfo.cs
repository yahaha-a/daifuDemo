using UnityEngine;

namespace daifuDemo
{
    public enum GunState
    {
        Ready,
        Aim,
        Revolve,
        Shooting,
        Cooling
    }
    
    public interface IGunInfo
    {
        string Name { get; }
        
        string Key { get; }
        
        float IntervalBetweenShots { get; }
        
        float RotationRate { get; }
        
        IGunInfo WithName(string name);
        
        IGunInfo WithKey(string key);
        
        IGunInfo WithIntervalBetweenShots(float intervalBetweenShots);
        
        IGunInfo WithRotationRate(float rotationRate);
    }
    
    public class GunInfo : IGunInfo
    {
        public string Name { get; private set; }
        
        public string Key { get; private set; }
        
        public float IntervalBetweenShots { get; private set; }
        
        public float RotationRate { get; private set; }
        
        public IGunInfo WithName(string name)
        {
            Name = name;
            return this;
        }

        public IGunInfo WithKey(string key)
        {
            Key = key;
            return this;
        }

        public IGunInfo WithIntervalBetweenShots(float intervalBetweenShots)
        {
            IntervalBetweenShots = intervalBetweenShots;
            return this;
        }

        public IGunInfo WithRotationRate(float rotationRate)
        {
            RotationRate = rotationRate;
            return this;
        }
    }
}