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
                                .WithNormalFishPieces((BackPackItemConfig.NormalFishPiecesKey, 1))
                        },
                        
                        {
                            2, new HarvestNormalFishInfo()
                                .WithNormalFishPieces((BackPackItemConfig.NormalFishPiecesKey, 3))
                        },

                        {
                            3, new HarvestNormalFishInfo()
                                .WithNormalFishPieces((BackPackItemConfig.NormalFishPiecesKey, 5))
                        }
                    }
                },

                {
                    Config.PteroisKey, new Dictionary<int, IHarvestFishInfo>()
                    {
                        {
                            1, new HarvestPteroisInfo()
                                .WithPteroisPieces((BackPackItemConfig.PteroisFishPiecesKey, 1))
                        },

                        {
                            2, new HarvestPteroisInfo()
                                .WithPteroisPieces((BackPackItemConfig.PteroisFishPiecesKey, 2))
                        },

                        {
                            3, new HarvestPteroisInfo()
                                .WithPteroisPieces((BackPackItemConfig.PteroisFishPiecesKey, 6))
                        }
                    }
                }
            };

        public Dictionary<string, int> HarvestItems { get; } = new Dictionary<string, int>();
        
        protected override void OnInit()
        {
            var fishSystem = this.GetSystem<IFishSystem>();

            var backPackSystem = this.GetSystem<IBackPackSystem>();

            Events.GamePass.Register(() =>
            {
                foreach (var (key, caughtFish) in fishSystem.CaughtFish)
                {
                    var harvestFish = HarvestFishInfos[caughtFish.FishKey][caughtFish.Star];
                    var harvestFishAmount = caughtFish.Amount;
                    
                    foreach (var (itemKey, count) in harvestFish.ItemList)
                    {
                        if (HarvestItems.ContainsKey(itemKey))
                        {
                            HarvestItems[itemKey] += count * harvestFishAmount;
                        }
                        else
                        {
                            HarvestItems.Add(itemKey, count * harvestFishAmount);
                        }

                        backPackSystem.AddBackPackItemList(itemKey, count * harvestFishAmount);
                        backPackSystem.SaveData();
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