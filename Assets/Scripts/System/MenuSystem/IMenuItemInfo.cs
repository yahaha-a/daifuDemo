using System.Collections.Generic;
using UnityEngine;

namespace daifuDemo
{
    public interface IMenuItemInfo
    {
        string Name { get; }
        
        Sprite Icon { get; }
        
        string Description { get; }
        
        float Score { get; }
        
        float Copies { get; }
        
        int UnLockNeed { get; }
        
        List<(int, float)> RankWithCost { get; }
        
        List<(int, string, int)> UpgradeNeedItems { get; }
        
        List<(string, int)> RequiredIngredientsAmount { get; }

        IMenuItemInfo WithName(string name);

        IMenuItemInfo WithIcon(Sprite icon);

        IMenuItemInfo WithDescription(string description);

        IMenuItemInfo WithScore(float score);

        IMenuItemInfo WithCopies(float copies);

        IMenuItemInfo WithUnlockNeed(int unlockNeed);

        IMenuItemInfo WithRankWithCost(List<(int, float)> rankWithCost);

        IMenuItemInfo WithUpgradeNeedItems(List<(int, string, int)> upgradeNeedItems);

        IMenuItemInfo WithRequiredIngredientsAmount(List<(string, int)> requiredIngredientsAmount);
    }
    
    public class MenuItemInfo : IMenuItemInfo
    {
        public string Name { get; private set; }
        
        public Sprite Icon { get; private set; }
        
        public string Description { get; private set; }
        
        public float Score { get; private set; }
        
        public float Copies { get; private set; }

        public int UnLockNeed { get; private set; }

        public List<(int, float)> RankWithCost { get; private set; }
        
        public List<(int, string, int)> UpgradeNeedItems { get; private set; }
        
        public List<(string, int)> RequiredIngredientsAmount { get; private set; }
        
        public IMenuItemInfo WithName(string name)
        {
            Name = name;
            return this;
        }

        public IMenuItemInfo WithIcon(Sprite icon)
        {
            Icon = icon;
            return this;
        }

        public IMenuItemInfo WithDescription(string description)
        {
            Description = description;
            return this;
        }

        public IMenuItemInfo WithScore(float score)
        {
            Score = score;
            return this;
        }

        public IMenuItemInfo WithCopies(float copies)
        {
            Copies = copies;
            return this;
        }

        public IMenuItemInfo WithUnlockNeed(int unlockNeed)
        {
            UnLockNeed = unlockNeed;
            return this;
        }

        public IMenuItemInfo WithRankWithCost(List<(int, float)> rankWithCost)
        {
            RankWithCost = rankWithCost;
            return this;
        }

        public IMenuItemInfo WithUpgradeNeedItems(List<(int, string, int)> upgradeNeedItems)
        {
            UpgradeNeedItems = upgradeNeedItems;
            return this;
        }


        public IMenuItemInfo WithRequiredIngredientsAmount(List<(string, int)> requiredIngredientsAmount)
        {
            RequiredIngredientsAmount = requiredIngredientsAmount;
            return this;
        }
    }
}