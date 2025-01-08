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
        
        float Price { get; }

        IBulletInfo WithType(BulletType type);

        IBulletInfo WithName(string name);
        
        IBulletInfo WithDamage(float damage);

        IBulletInfo WithPrice(float price);
    }

    public class BulletInfo : IBulletInfo
    {
        public BulletType Type { get; private set; }
        public string Name { get; private set; }
        
        public float Damage { get; private set; }
        
        public float Price { get; private set; }

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

        public IBulletInfo WithPrice(float price)
        {
            Price = price;
            return this;
        }
    }
}