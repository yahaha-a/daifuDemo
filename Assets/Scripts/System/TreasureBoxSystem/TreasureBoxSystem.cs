using System.Collections.Generic;
using Global;
using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public interface ITreasureBoxSystem : ISystem
    {
        Dictionary<string, ITreasureItemInfo> TreasureItemInfos { get; }

        ITreasureBoxSystem AddTreasureItemInfos(string key, ITreasureItemInfo treasureBoxInfo);

        ITreasureItemInfo FindTreasureItemInfo(string key);
    }
    
    public class TreasureBoxSystem : AbstractSystem, ITreasureBoxSystem
    {
        protected override void OnInit()
        {
            //TODO
            this.AddTreasureItemInfos(Config.ToolTreasureChestKey, new TreasureItemInfo()
                    .WithName("材料宝箱")
                    .WithOpenNeedSeconds(2f)
                    .WithPossessionItemType(BackPackItemType.Tool)
                    .WithNumber(10))
                .AddTreasureItemInfos(Config.SpiceTreasureChestKey, new TreasureItemInfo()
                    .WithName("调料宝箱")
                    .WithOpenNeedSeconds(3f)
                    .WithPossessionItemType(BackPackItemType.Seasoning)
                    .WithNumber(1))
                .AddTreasureItemInfos(Config.WeaponLevelTreasureChestKey, new TreasureItemInfo()
                    .WithName("武器等级宝箱")
                    .WithOpenNeedSeconds(4f)
                    .WithPossessionItemType(BackPackItemType.WeaponLevel)
                    .WithNumber(1));
        }

        public Dictionary<string, ITreasureItemInfo> TreasureItemInfos { get; } =
            new Dictionary<string, ITreasureItemInfo>();

        public ITreasureBoxSystem AddTreasureItemInfos(string key, ITreasureItemInfo treasureBoxInfo)
        {
            TreasureItemInfos.Add(key, treasureBoxInfo);
            return this;
        }

        public ITreasureItemInfo FindTreasureItemInfo(string key)
        {
            return TreasureItemInfos[key];
        }
    }
}