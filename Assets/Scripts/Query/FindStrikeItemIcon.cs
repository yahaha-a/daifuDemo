using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public class FindStrikeItemIcon : AbstractQuery<Sprite>
    {
        private string _key;
        
        public FindStrikeItemIcon(string key)
        {
            _key = key;
        }
        
        protected override Sprite OnDo()
        {
            var fishSystem = this.GetSystem<IStrikeItemSystem>();
            var icon = fishSystem.StrikeItemInfos[_key].Icon;
            return icon;
        }
    }
}