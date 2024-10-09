using System.Collections.Generic;
using Global;
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

        private IUtils _utils;
        
        protected override void OnInit()
        {
            _utils = this.GetUtility<IUtils>();
            
            //TODO
            this.AddStrikeItemInfos(Config.KelpPlantsKey, new StrikeItemInfo()
                    .WithPluckItemType(PluckItemType.Plant)
                    .WithName("海带植物")
                    .WithIcon(_utils.AdjustSprite(_resLoader.LoadSync<Texture2D>(Config.KelpPlantsKey)))
                    .WithDropItemKey(BackPackItemConfig.KelpKey)
                    .WithDropAmountWithTimes(new List<(int, int)>() { (1, 3) }))
                .AddStrikeItemInfos(Config.CoralPlantsKey, new StrikeItemInfo()
                    .WithPluckItemType(PluckItemType.Plant)
                    .WithName("珊瑚礁")
                    .WithIcon(_utils.AdjustSprite(_resLoader.LoadSync<Texture2D>(Config.CoralPlantsKey)))
                    .WithDropItemKey(BackPackItemConfig.CoralKey)
                    .WithDropAmountWithTimes(new List<(int, int)>() { (1, 1), (1, 2) }))
                .AddStrikeItemInfos(Config.CopperOreKey, new StrikeItemInfo()
                    .WithPluckItemType(PluckItemType.Ore)
                    .WithName("铜矿")
                    .WithIcon(_utils.AdjustSprite(_resLoader.LoadSync<Texture2D>(Config.CopperOreKey)))
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