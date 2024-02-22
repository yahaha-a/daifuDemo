using UnityEngine;

namespace daifuDemo
{
    public interface ICaughtItemInfo
    {
        string FishKey { get; }
        
        string FishName { get; }
        
        Sprite FishIcon { get; }
        
        int Star { get; }
        
        int Amount { get; set; }

        ICaughtItemInfo WithFishKey(string fishKey);

        ICaughtItemInfo WithFishName(string fishName);

        ICaughtItemInfo WithFishIcon(Sprite fishIcon);

        ICaughtItemInfo WithStar(int star);

        ICaughtItemInfo WithAmount(int amount);
    }

    public class CaughtItemInfo : ICaughtItemInfo
    {
        public string FishKey { get; private set; }
        
        public string FishName { get; private set; }
        
        public Sprite FishIcon { get; private set; }
        
        public int Star { get; private set; }
        
        public int Amount { get; set; }

        public ICaughtItemInfo WithFishKey(string fishKey)
        {
            FishKey = fishKey;
            return this;
        }

        public ICaughtItemInfo WithFishName(string fishName)
        {
            FishName = fishName;
            return this;
        }

        public ICaughtItemInfo WithFishIcon(Sprite fishIcon)
        {
            FishIcon = fishIcon;
            return this;
        }

        public ICaughtItemInfo WithStar(int star)
        {
            Star = star;
            return this;
        }

        public ICaughtItemInfo WithAmount(int amount)
        {
            Amount = amount;
            return this;
        }
    }
}