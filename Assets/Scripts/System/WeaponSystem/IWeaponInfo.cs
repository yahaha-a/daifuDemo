namespace daifuDemo
{
    public interface IWeaponInfo
    {
        string Key { get; }
        
        string Name { get; }
        
        WeaponType Type { get; }
        
        int Rank { get; }

        IWeaponInfo WithKey(string key);
        
        IWeaponInfo WithName(string name);

        IWeaponInfo WithType(WeaponType type);

        IWeaponInfo WithRank(int rank);
    }
    
    public class WeaponInfo<T> : IWeaponInfo where T : WeaponInfo<T>
    {
        public string Key { get; private set; }
        
        public string Name { get; private set; }
        
        public WeaponType Type { get; private set; }
        
        public int Rank { get; private set; }

        public T WithKey(string key)
        {
            Key = key;
            return (T)this;
        }

        public T WithName(string name)
        {
            Name = name;
            return (T)this;
        }

        public T WithType(WeaponType type)
        {
            Type = type;
            return (T)this;
        }

        public T WithRank(int rank)
        {
            Rank = rank;
            return (T)this;
        }

        IWeaponInfo IWeaponInfo.WithKey(string key)
        {
            return WithKey(key);
        }

        IWeaponInfo IWeaponInfo.WithName(string name)
        {
            return WithName(name);
        }

        IWeaponInfo IWeaponInfo.WithType(WeaponType type)
        {
            return WithType(type);
        }

        IWeaponInfo IWeaponInfo.WithRank(int rank)
        {
            return WithRank(rank);
        }
    }
}