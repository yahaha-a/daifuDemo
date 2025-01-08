namespace daifuDemo
{
    public interface IFishForkInfo
    {
        float LaunchSpeed { get; }
        
        float FishForkLength { get; }
        
        float ChargingTime { get; }
        
        IFishForkInfo WithLaunchSpeed(float launchSpeed);

        IFishForkInfo WithFishForkLength(float fishForkLength);

        IFishForkInfo WithChargingTime(float chargingTime);
    }
    
    public class FishForkInfo : WeaponInfo<FishForkInfo>, IFishForkInfo
    {
        public float LaunchSpeed { get; private set; }
        
        public float FishForkLength { get; private set; }
        
        public float ChargingTime { get; private set; }

        public FishForkInfo WithLaunchSpeed(float launchSpeed)
        {
            LaunchSpeed = launchSpeed;
            return this;
        }

        public FishForkInfo WithFishForkLength(float fishForkLength)
        {
            FishForkLength = fishForkLength;
            return this;
        }

        public FishForkInfo WithChargingTime(float chargingTime)
        {
            ChargingTime = chargingTime;
            return this;
        }

        IFishForkInfo IFishForkInfo.WithLaunchSpeed(float launchSpeed)
        {
            return WithLaunchSpeed(launchSpeed);
        }

        IFishForkInfo IFishForkInfo.WithFishForkLength(float fishForkLength)
        {
            return WithFishForkLength(fishForkLength);
        }
        
        IFishForkInfo IFishForkInfo.WithChargingTime(float chargingTime)
        {
            return WithChargingTime(chargingTime);
        }
    }
}