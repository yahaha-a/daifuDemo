namespace daifuDemo
{
    public enum BulletAttribute
    {
        Normal,
        Hypnosis,
        ArmorPiercing,
    }
    
    public interface IBulletInfo
    {
        float Damage { get; }
        
        float Speed { get; }
        
        float Range { get; }

        IBulletInfo WithDamage(float damage);

        IBulletInfo WithSpeed(float speed);

        IBulletInfo WithRange(float range);
    }

    public class BulletInfo : IBulletInfo
    {
        public float Damage { get; private set; }
        
        public float Speed { get; private set; }
        
        public float Range { get; private set; }
        
        public IBulletInfo WithDamage(float damage)
        {
            Damage = damage;
            return this;
        }

        public IBulletInfo WithSpeed(float speed)
        {
            Speed = speed;
            return this;
        }

        public IBulletInfo WithRange(float range)
        {
            Range = range;
            return this;
        }
    }
}