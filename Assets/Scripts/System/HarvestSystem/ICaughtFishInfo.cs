using UnityEngine;

namespace daifuDemo
{
    public interface ICaughtFishInfo
    {
        string FishKey { get; }
        
        string FishName { get; }
        
        Sprite FishIcon { get; }
        
        int Star { get; }
        
        int Amount { get; set; }

        ICaughtFishInfo WithFishKey(string fishKey);

        ICaughtFishInfo WithFishName(string fishName);

        ICaughtFishInfo WithFishIcon(Sprite fishIcon);

        ICaughtFishInfo WithStar(int star);

        ICaughtFishInfo WithAmount(int amount);
    }

    public class CaughtFishInfo : ICaughtFishInfo
    {
        public string FishKey { get; private set; }
        
        public string FishName { get; private set; }
        
        public Sprite FishIcon { get; private set; }
        
        public int Star { get; private set; }
        
        public int Amount { get; set; }

        public ICaughtFishInfo WithFishKey(string fishKey)
        {
            FishKey = fishKey;
            return this;
        }

        public ICaughtFishInfo WithFishName(string fishName)
        {
            FishName = fishName;
            return this;
        }

        public ICaughtFishInfo WithFishIcon(Sprite fishIcon)
        {
            FishIcon = fishIcon;
            return this;
        }

        public ICaughtFishInfo WithStar(int star)
        {
            Star = star;
            return this;
        }

        public ICaughtFishInfo WithAmount(int amount)
        {
            Amount = amount;
            return this;
        }
    }
}