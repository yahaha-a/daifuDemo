using UnityEngine;

namespace daifuDemo
{
    public enum CreateItemType
    {
        Fish,
        TreasureChest,
        Destructible
    }
    
    public interface IMapCreateItemInfo
    {
        string Key { get; }
        
        GameObject Prefab { get; }
        
        CreateItemType Type { get; }
        
        Vector3 Position { get; }
        
        float Range { get; }
        
        int Number { get; }

        IMapCreateItemInfo WithKey(string key);

        IMapCreateItemInfo WithPrefab(GameObject prefab);

        IMapCreateItemInfo WithType(CreateItemType type);

        IMapCreateItemInfo WithPosition(Vector3 position);

        IMapCreateItemInfo WithRange(float range);

        IMapCreateItemInfo WithNumber(int number);
    }

    public class MapCreateItemInfo : IMapCreateItemInfo
    {
        public string Key { get; set; }
        
        public GameObject Prefab { get; set; }
        
        public CreateItemType Type { get; set; }

        public Vector3 Position { get; set; }
        
        public float Range { get; set; }
        
        public int Number { get; set; }

        public IMapCreateItemInfo WithKey(string key)
        {
            Key = key;
            return this;
        }

        public IMapCreateItemInfo WithPrefab(GameObject prefab)
        {
            Prefab = prefab;
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