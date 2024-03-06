using System.Linq;
using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public class FindMenuItemCostWithRank : AbstractQuery<float>
    {
        private string _menuKey;

        public FindMenuItemCostWithRank(string menuKey)
        {
            _menuKey = menuKey;
        }
        
        protected override float OnDo()
        {
            var menuSystem = this.GetSystem<IMenuSystem>();

            float cost = menuSystem.MenuItemInfos[_menuKey].RankWithCost
                .FirstOrDefault(item => item.Item1 == menuSystem.CurrentOwnMenuItems[_menuKey].Rank.Value).Item2;

            return cost;
        }
    }
}