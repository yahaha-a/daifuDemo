namespace daifuDemo
{
    public interface IMeleeWeapons
    {
        bool IfLeft { get; }
        
        float Damage { get; }

        float AttackRadius { get; }

        float AttackFrequency { get; }
    }
}