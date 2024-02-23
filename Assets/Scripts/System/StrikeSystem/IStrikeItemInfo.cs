using System.Collections.Generic;
using UnityEngine;

namespace daifuDemo
{
    public enum PluckItemType
    {
        Plant,
        Ore
    }
    
    public interface IStrikeItemInfo
    {
        PluckItemType Type { get; }
        
        string Name { get; }
        
        Sprite Icon { get; }
        
        string DropItemKey { get; }
        
        List<(int, int)> DropAmountWithTimes { get; }

        IStrikeItemInfo WithPluckItemType(PluckItemType type);

        IStrikeItemInfo WithName(string name);

        IStrikeItemInfo WithIcon(Sprite icon);

        IStrikeItemInfo WithDropItemKey(string dropItemKey);

        IStrikeItemInfo WithDropAmountWithTimes(List<(int, int)> dropAmountWithTimes);
    }

    public class StrikeItemInfo : IStrikeItemInfo
    {
        public PluckItemType Type { get; private set; }
        
        public string Name { get; private set; }
        
        public Sprite Icon { get; private set; }

        public string DropItemKey { get; private set; }
        
        public List<(int, int)> DropAmountWithTimes { get; private set; }
        
        public IStrikeItemInfo WithPluckItemType(PluckItemType type)
        {
            Type = type;
            return this;
        }

        public IStrikeItemInfo WithName(string name)
        {
            Name = name;
            return this;
        }

        public IStrikeItemInfo WithIcon(Sprite icon)
        {
            Icon = icon;
            return this;
        }

        public IStrikeItemInfo WithDropItemKey(string dropItemKey)
        {
            DropItemKey = dropItemKey;
            return this;
        }

        public IStrikeItemInfo WithDropAmountWithTimes(List<(int, int)> dropAmountWithTimes)
        {
            DropAmountWithTimes = dropAmountWithTimes;
            return this;
        }
    }
}