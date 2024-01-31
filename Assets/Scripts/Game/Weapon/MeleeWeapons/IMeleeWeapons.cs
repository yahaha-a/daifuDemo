namespace daifuDemo
{
    public interface IMeleeWeapons : Iweapon
    {
        bool IfLeft { get; }
        
        float Damage { get; }

        float AttackRadius { get; }

        float AttackFrequency { get; }
    }
}