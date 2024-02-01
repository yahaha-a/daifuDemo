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
        BulletAttribute Attribute { get; }
        
        float Damage { get; }
    }
}