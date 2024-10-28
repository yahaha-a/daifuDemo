namespace daifuDemo
{
    public interface IFishForkInfo
    {
        float RotationRate { get; }
        
        float Speed { get; }
        
        float FishForkLength { get; }
        
        IFishForkInfo WithRotationRate(float rotationRate);

        IFishForkInfo WithSpeed(float speed);

        IFishForkInfo WithFishForkLength(float fishForkLength);
    }
    
    public class FishForkInfo : WeaponInfo<FishForkInfo>, IFishForkInfo
    {
        public float RotationRate { get; private set; }
        
        public float Speed { get; private set; }
        
        public float FishForkLength { get; private set; }

        public FishForkInfo WithRotationRate(float rotationRate)
        {
            RotationRate = rotationRate;
            return this;
        }

        public FishForkInfo WithSpeed(float speed)
        {
            Speed = speed;
            return this;
        }

        public FishForkInfo WithFishForkLength(float fishForkLength)
        {
            FishForkLength = fishForkLength;
            return this;
        }

        IFishForkInfo IFishForkInfo.WithRotationRate(float rotationRate)
        {
            return WithRotationRate(rotationRate);
        }

        IFishForkInfo IFishForkInfo.WithSpeed(float speed)
        {
            return WithSpeed(speed);
        }

        IFishForkInfo IFishForkInfo.WithFishForkLength(float fishForkLength)
        {
            return WithFishForkLength(fishForkLength);
        }
    }
}