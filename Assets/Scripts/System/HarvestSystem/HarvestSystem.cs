using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public interface IHarvestSystem : ISystem
    {
        Dictionary<string, Dictionary<int, IHarvestFishInfo>> HarvestFishInfos { get; }
        
        Dictionary<string, int> HarvestItems { get; }

        IHarvestFishInfo FindHarvestFishInfo(string fishKey, int star);

        void Reload();
    }
    
    public class HarvestSystem : AbstractSystem, IHarvestSystem
    {
        public Dictionary<string, Dictionary<int, IHarvestFishInfo>> HarvestFishInfos { get; } =
            new Dictionary<string, Dictionary<int, IHarvestFishInfo>>()
            {
                {
                    Config.NormalFishKey, new Dictionary<int, IHarvestFishInfo>()
                    {
                        {
                            1, new HarvestNormalFishInfo()
                                .WithNormalFishPieces(("普通鱼块", 1))
                        },
                        
                        {
                            2, new HarvestNormalFishInfo()
                                .WithNormalFishPieces(("普通鱼块", 3))
                        },

                        {
                            3, new HarvestNormalFishInfo()
                                .WithNormalFishPieces(("普通鱼块", 5))
                        }
                    }
                },

                {
                    Config.PteroisKey, new Dictionary<int, IHarvestFishInfo>()
                    {
                        {
                            1, new HarvestPteroisInfo()
                                .WithPteroisPieces(("狮子鱼块", 1))
                        },

                        {
                            2, new HarvestPteroisInfo()
                                .WithPteroisPieces(("狮子鱼块", 2))
                        },

                        {
                            3, new HarvestPteroisInfo()
                                .WithPteroisPieces(("狮子鱼块", 6))
                        }
                    }
                }
            };

        public Dictionary<string, int> HarvestItems { get; } = new Dictionary<string, int>();
        
        protected override void OnInit()
        {
            var fishSystem = this.GetSystem<IFishSystem>();

            Events.GamePass.Register(() =>
            {
                foreach (var (key, caughtFish) in fishSystem.CaughtFish)
                {
                    var harvestFish = HarvestFishInfos[caughtFish.FishKey][caughtFish.Star];
                    var harvestFishAmount = caughtFish.Amount;
                    foreach (var itemDictionary in harvestFish.ItemList)
                    {
                        foreach (var itemName in itemDictionary.Keys)
                        {
                            if (HarvestItems.ContainsKey(itemName))
                            {
                                HarvestItems[itemName] += itemDictionary[itemName] * harvestFishAmount;
                            }
                            else
                            {
                                HarvestItems.Add(itemName, itemDictionary[itemName] * harvestFishAmount);
                            }
                        }
                    }
                }
            });
        }

        public IHarvestFishInfo FindHarvestFishInfo(string fishKey, int star)
        {
            return HarvestFishInfos[fishKey][star];
        }

        public void Reload()
        {
            HarvestItems.Clear();
        }
    }
}