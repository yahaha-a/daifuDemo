using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public class FindFishIcon : AbstractQuery<Texture2D>
    {
        private string _fishKey;
        
        public FindFishIcon(string fishKey)
        {
            _fishKey = fishKey;
        }
        
        protected override Texture2D OnDo()
        {
            var fishSystem = this.GetSystem<IFishSystem>();
            var icon = fishSystem.FishInfos[_fishKey].FishIcon;
            return icon;
        }
    }
}