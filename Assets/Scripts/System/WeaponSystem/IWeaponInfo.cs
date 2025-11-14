using UnityEngine;

namespace daifuDemo
{
    public interface IWeaponInfo
    {
        string Key { get; }
        
        string Name { get; }
        
        WeaponType Type { get; }
        
        int Rank { get; }
        
        int MaxRank { get; }
        
        Texture2D Icon { get; }
        
        IWeaponInfo WithKey(string key);
        
        IWeaponInfo WithName(string name);

        IWeaponInfo WithType(WeaponType type);

        IWeaponInfo WithRank(int rank);
        
        IWeaponInfo WithMaxRank(int maxRank);

        IWeaponInfo WithIcon(Texture2D icon);
    }
    
    public class WeaponInfo<T> : IWeaponInfo where T : WeaponInfo<T>
    {
        public string Key { get; private set; }
        
        public string Name { get; private set; }
        
        public WeaponType Type { get; private set; }
        
        public int Rank { get; private set; }
        
        public int MaxRank { get; private set; }

        public Texture2D Icon { get; private set; }

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

        public T WithMaxRank(int maxRank)
        {
            MaxRank = maxRank;
            return (T)this;
        }

        public T WithIcon(Texture2D icon)
        {
            Icon = icon;
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

        IWeaponInfo IWeaponInfo.WithMaxRank(int maxRank)
        {
            return WithMaxRank(maxRank);
        }

        IWeaponInfo IWeaponInfo.WithIcon(Texture2D icon)
        {
            return WithIcon(icon);
        }
    }
}