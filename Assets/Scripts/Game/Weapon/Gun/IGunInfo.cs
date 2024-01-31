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
        float Name { get; }
        
        float Key { get; }
        
        IBullet CurrentBullet { get; }
        
        float IntervalBetweenShots { get; }
        
        float RotationRate { get; }

        IGunInfo WithName(float name);
        
        IGunInfo WithKey(float key);
        
        IGunInfo WithCurrentBullet(IBullet currentBullet);
        
        IGunInfo WithIntervalBetweenShots(float intervalBetweenShots);
        
        IGunInfo WithRotationRate(float rotationRate);
    }
    
    public class GunInfo : IGunInfo
    {
        public float Name { get; private set; }
        
        public float Key { get; private set; }
        
        public IBullet CurrentBullet { get; private set; }
        
        public float IntervalBetweenShots { get; private set; }
        
        public float RotationRate { get; private set; }
        
        public IGunInfo WithName(float name)
        {
            Name = name;
            return this;
        }

        public IGunInfo WithKey(float key)
        {
            Key = key;
            return this;
        }

        public IGunInfo WithCurrentBullet(IBullet currentBullet)
        {
            CurrentBullet = currentBullet;
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