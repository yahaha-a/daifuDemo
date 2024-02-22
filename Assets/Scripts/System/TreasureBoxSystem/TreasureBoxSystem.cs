using System.Collections.Generic;
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
            this.AddTreasureItemInfos(Config.SpiceTreasureChestKey, new TreasureItemInfo()
                .WithName("调料宝箱")
                .WithOpenNeedSeconds(2f)
                .WithPossessionItemType(BackPackItemType.Seasoning));
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