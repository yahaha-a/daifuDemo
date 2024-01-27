using System;
using System.Linq;
using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public class FindARandomFishPrefab : AbstractQuery<GameObject>
    {
        protected override GameObject OnDo()
        {
            var fishSystem = this.GetSystem<IFishSystem>();
            var fishInfo = fishSystem.FishInfos.OrderBy(kv => Guid.NewGuid()).First().Value.FishPrefab;
            return fishInfo;
        }
    }
}