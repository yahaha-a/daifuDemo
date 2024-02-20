using UnityEngine;

namespace daifuDemo
{
    public enum BackPackItemType
    {
        Fish,
        Ingredient,
        Seasoning,
        Tool,
        Weapon
    }
    
    public interface IBackPackItemInfo
    {
        string ItemKey { get; }
        
        string ItemName { get; }
        
        Sprite ItemIcon { get; }
        
        BackPackItemType ItemType { get; }
        
        string ItemDescription { get; }

        IBackPackItemInfo WithItemKey(string key);

        IBackPackItemInfo WithItemName(string name);

        IBackPackItemInfo WithItemIcon(Sprite icon);

        IBackPackItemInfo WithItemType(BackPackItemType itemType);

        IBackPackItemInfo WithItemDescription(string description);
    }

    public class BackPackItemInfo : IBackPackItemInfo
    {
        public string ItemKey { get; private set; }
        
        public string ItemName { get; private set; }
        
        public Sprite ItemIcon { get; private set; }
        
        public BackPackItemType ItemType { get; private set; }

        public string ItemDescription { get; private set; }
        
        public IBackPackItemInfo WithItemKey(string key)
        {
            ItemKey = key;
            return this;
        }

        public IBackPackItemInfo WithItemName(string name)
        {
            ItemName = name;
            return this;
        }

        public IBackPackItemInfo WithItemIcon(Sprite icon)
        {
            ItemIcon = icon;
            return this;
        }

        public IBackPackItemInfo WithItemType(BackPackItemType itemType)
        {
            ItemType = itemType;
            return this;
        }

        public IBackPackItemInfo WithItemDescription(string description)
        {
            ItemDescription = description;
            return this;
        }
    }
}