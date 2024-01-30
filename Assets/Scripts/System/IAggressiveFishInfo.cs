namespace daifuDemo
{
    public interface IAggressiveFishInfo : IFishInfo
    {
        float Damage { get; }

        float PursuitSwimRate { get; }

        IAggressiveFishInfo WithDamage(float damage);

        IAggressiveFishInfo WithPursuitSwimRate(float pursuitSwimRate);
    }

    public class AggressiveFishInfo : FishInfo, IAggressiveFishInfo
    {
        public float Damage { get; private set; }
        
        public float PursuitSwimRate { get; private set; }
        
        public IAggressiveFishInfo WithDamage(float damage)
        {
            Damage = damage;
            return this;
        }
        
        public IAggressiveFishInfo WithPursuitSwimRate(float pursuitSwimRate)
        {
            PursuitSwimRate = pursuitSwimRate;
            return this;
        }
    }
}