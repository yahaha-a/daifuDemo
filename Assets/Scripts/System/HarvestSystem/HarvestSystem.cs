using System.Collections.Generic;
using Global;
using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public interface IHarvestSystem : ISystem
    {
        Dictionary<string, IHarvestInfo> HarvestFishInfos { get; }
        
        Dictionary<string, int> HarvestItems { get; }
        
        IHarvestSystem AddHarvestInfos(string key, IHarvestInfo infos);

        void Reload();
    }
    
    public class HarvestSystem : AbstractSystem, IHarvestSystem
    {
        public Dictionary<string, IHarvestInfo> HarvestFishInfos { get; } =
            new Dictionary<string, IHarvestInfo>();

        public Dictionary<string, int> HarvestItems { get; } = new Dictionary<string, int>();

        protected override void OnInit()
        {
            //TODO
            this.AddHarvestInfos(Config.NormalFishKey, new HarvestInfo()
                    .WithCount(new List<(string, int, int)>()
                    {
                        (BackPackItemConfig.NormalFishPiecesKey, 1, 1),
                        (BackPackItemConfig.NormalFishPiecesKey, 2, 3),
                        (BackPackItemConfig.NormalFishPiecesKey, 3, 5)
                    }))
                .AddHarvestInfos(Config.AggressiveFishKey, new HarvestInfo()
                    .WithCount(new List<(string, int, int)>()
                    {
                        (BackPackItemConfig.PteroisFishPiecesKey, 1, 1),
                        (BackPackItemConfig.PteroisFishPiecesKey, 2, 2),
                        (BackPackItemConfig.PteroisFishPiecesKey, 3, 6)
                    }))
                
                .AddHarvestInfos(BackPackItemConfig.KelpKey, new HarvestInfo()
                    .WithCount(new List<(string, int, int)>()
                    {
                        (BackPackItemConfig.KelpKey, 0, 1)
                    }))
                .AddHarvestInfos(BackPackItemConfig.CopperKey, new HarvestInfo()
                    .WithCount(new List<(string, int, int)>()
                    {
                        (BackPackItemConfig.CopperKey, 0, 1)
                    }))
                .AddHarvestInfos(BackPackItemConfig.CoralKey, new HarvestInfo()
                    .WithCount(new List<(string, int, int)>()
                    {
                        (BackPackItemConfig.CoralKey, 0, 1)
                    }))
                
                .AddHarvestInfos(BackPackItemConfig.CordageKey, new HarvestInfo()
                    .WithCount(new List<(string, int, int)>()
                    {
                        (BackPackItemConfig.CordageKey, 0, 1)
                    }))
                .AddHarvestInfos(BackPackItemConfig.WoodKey, new HarvestInfo()
                    .WithCount(new List<(string, int, int)>()
                    {
                        (BackPackItemConfig.WoodKey, 0, 1)
                    }))
                
                .AddHarvestInfos(BackPackItemConfig.SaltKey, new HarvestInfo()
                    .WithCount(new List<(string, int, int)>()
                    {
                        (BackPackItemConfig.SaltKey, 0, 1)
                    }))
                .AddHarvestInfos(BackPackItemConfig.VinegarKey, new HarvestInfo()
                    .WithCount(new List<(string, int, int)>()
                    {
                        (BackPackItemConfig.VinegarKey, 0, 1)
                    }));
            
            var fishSystem = this.GetSystem<IFishSystem>();

            var backPackSystem = this.GetSystem<IBackPackSystem>();

            var archiveSystem = this.GetSystem<IArchiveSystem>();

            Events.GamePass.Register(() =>
            {
                foreach (var (key, caughtFish) in fishSystem.CaughtItem)
                {
                    var harvestFish = HarvestFishInfos[caughtFish.FishKey];
                    var harvestFishAmount = caughtFish.Amount;
                    
                    foreach (var (backPackItemKey, star, count) in harvestFish.Count)
                    {
                        if (caughtFish.Star == star)
                        {
                            if (HarvestItems.ContainsKey(backPackItemKey))
                            {
                                HarvestItems[backPackItemKey] += count * harvestFishAmount;
                            }
                            else
                            {
                                HarvestItems.Add(backPackItemKey, count * harvestFishAmount);
                            }
                            backPackSystem.AddBackPackItemList(backPackItemKey, count * harvestFishAmount);
                        }
                    }
                }
                
                archiveSystem.SaveData(backPackSystem.ShipBackPackItemList, "ShipBackPackItemList");
                archiveSystem.SaveData(backPackSystem.SuShiBackPackItemList, "SuShiBackPackItemList");
            });
        }

        public IHarvestSystem AddHarvestInfos(string key, IHarvestInfo harvestInfo)
        {
            HarvestFishInfos.Add(key, harvestInfo);
            return this;
        }

        public void Reload()
        {
            HarvestItems.Clear();
        }
    }
}