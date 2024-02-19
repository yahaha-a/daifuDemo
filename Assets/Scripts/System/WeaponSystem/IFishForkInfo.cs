namespace daifuDemo
{
    public interface IFishForkInfo
    {
        string Name { get; }
        
        float RotationRate { get; }
        
        float Speed { get; }
        
        float FishForkLength { get; }

        IFishForkInfo WithName(string name);
        
        IFishForkInfo WithRotationRate(float rotationRate);

        IFishForkInfo WithSpeed(float speed);

        IFishForkInfo WithFishForkLength(float fishForkLength);
    }
    
    public class FishForkInfo : IFishForkInfo
    {
        public string Name { get; private set; }
        
        public float RotationRate { get; private set; }
        
        public float Speed { get; private set; }
        
        public float FishForkLength { get; private set; }

        public IFishForkInfo WithName(string name)
        {
            Name = name;
            return this;
        }

        public IFishForkInfo WithRotationRate(float rotationRate)
        {
            RotationRate = rotationRate;
            return this;
        }

        public IFishForkInfo WithSpeed(float speed)
        {
            Speed = speed;
            return this;
        }

        public IFishForkInfo WithFishForkLength(float fishForkLength)
        {
            FishForkLength = fishForkLength;
            return this;
        }
    }
}