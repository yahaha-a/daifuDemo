using UnityEngine;

namespace daifuDemo
{
    public enum CreateItemType
    {
        Null,
        Barrier,
        Role,
        Fish,
        TreasureChests,
        Destructible,
        Drops
    }
    
    public interface IMapCreateItemInfo
    {
        string Key { get; }
        
        CreateItemType Type { get; }
        
        Vector3 Position { get; }
        
        float Range { get; }
        
        int Number { get; }

        IMapCreateItemInfo WithKey(string key);

        IMapCreateItemInfo WithType(CreateItemType type);

        IMapCreateItemInfo WithPosition(Vector3 position);

        IMapCreateItemInfo WithRange(float range);

        IMapCreateItemInfo WithNumber(int number);
    }

    public class MapCreateItemInfo : IMapCreateItemInfo
    {
        public string Key { get; set; }
        
        public CreateItemType Type { get; set; }

        public Vector3 Position { get; set; }
        
        public float Range { get; set; }
        
        public int Number { get; set; }

        public IMapCreateItemInfo WithKey(string key)
        {
            Key = key;
            return this;
        }

        public IMapCreateItemInfo WithType(CreateItemType type)
        {
            Type = type;
            return this;
        }

        public IMapCreateItemInfo WithPosition(Vector3 position)
        {
            Position = position;
            return this;
        }

        public IMapCreateItemInfo WithRange(float range)
        {
            Range = range;
            return this;
        }

        public IMapCreateItemInfo WithNumber(int number)
        {
            Number = number;
            return this;
        }
    }
}