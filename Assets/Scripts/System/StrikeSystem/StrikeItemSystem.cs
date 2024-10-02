using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public interface IStrikeItemSystem : ISystem
    {
        Dictionary<string, IStrikeItemInfo> StrikeItemInfos { get; }

        IStrikeItemSystem AddStrikeItemInfos(string key, IStrikeItemInfo strikeItemInfo);
    }
    
    public class StrikeItemSystem : AbstractSystem, IStrikeItemSystem
    {
        private static ResLoader _resLoader = ResLoader.Allocate();
        protected override void OnInit()
        {
            this.AddStrikeItemInfos(Config.KelpPlantsKey, new StrikeItemInfo()
                    .WithPluckItemType(PluckItemType.Plant)
                    .WithName("海带植物")
                    .WithIcon(_resLoader.LoadSync<Sprite>("Kelp"))
                    .WithDropItemKey(BackPackItemConfig.KelpKey)
                    .WithDropAmountWithTimes(new List<(int, int)>() { (1, 3) }))
                .AddStrikeItemInfos(Config.CopperOreKey, new StrikeItemInfo()
                    .WithPluckItemType(PluckItemType.Ore)
                    .WithName("铜矿")
                    .WithIcon(_resLoader.LoadSync<Sprite>("Coral"))
                    .WithDropItemKey(BackPackItemConfig.CopperKey)
                    .WithDropAmountWithTimes(new List<(int, int)>() { (1, 1), (1, 1), (1, 1) }));
        }

        public Dictionary<string, IStrikeItemInfo> StrikeItemInfos { get; } = new Dictionary<string, IStrikeItemInfo>();
        
        public IStrikeItemSystem AddStrikeItemInfos(string key, IStrikeItemInfo strikeItemInfo)
        {
            StrikeItemInfos.Add(key, strikeItemInfo);
            return this;
        }
    }
}