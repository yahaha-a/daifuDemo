namespace daifuDemo
{
    public enum BulletType
    {
        Normal,
        Hypnosis,
    }
    
    public interface IBulletInfo
    {
        BulletType Type { get; }
        
        string Name { get; }
        
        float Damage { get; }

        IBulletInfo WithType(BulletType type);

        IBulletInfo WithName(string name);
        
        IBulletInfo WithDamage(float damage);
    }

    public class BulletInfo : IBulletInfo
    {
        public BulletType Type { get; private set; }
        public string Name { get; private set; }
        
        public float Damage { get; private set; }

        public IBulletInfo WithType(BulletType type)
        {
            Type = type;
            return this;
        }

        public IBulletInfo WithName(string name)
        {
            Name = name;
            return this;
        }

        public IBulletInfo WithDamage(float damage)
        {
            Damage = damage;
            return this;
        }
    }
}