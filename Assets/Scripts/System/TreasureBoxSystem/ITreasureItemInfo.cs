using System;

namespace daifuDemo
{
    public interface ITreasureItemInfo
    {
        string Name { get; }
        
        float OpenNeedSeconds { get; }
        
        BackPackItemType PossessionItemType { get; }
        
        int Number { get; }

        ITreasureItemInfo WithName(string name);

        ITreasureItemInfo WithOpenNeedSeconds(float openNeedSeconds);

        ITreasureItemInfo WithPossessionItemType(BackPackItemType possessionItemType);

        ITreasureItemInfo WithNumber(int number);
    }

    public class TreasureItemInfo : ITreasureItemInfo
    {
        public string Name { get; private set; }
        
        public float OpenNeedSeconds { get; private set; }
        
        public BackPackItemType PossessionItemType { get; private set; }
        
        public int Number { get; set; }

        public ITreasureItemInfo WithName(string name)
        {
            Name = name;
            return this;
        }

        public ITreasureItemInfo WithOpenNeedSeconds(float openNeedSeconds)
        {
            OpenNeedSeconds = openNeedSeconds;
            return this;
        }

        public ITreasureItemInfo WithPossessionItemType(BackPackItemType possessionItemType)
        {
            PossessionItemType = possessionItemType;
            return this;
        }

        public ITreasureItemInfo WithNumber(int number)
        {
            Number = number;
            return this;
        }
    }
}
