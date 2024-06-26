using QFramework;

namespace daifuDemo
{
    public class FindFishStruggleTime : AbstractQuery<float>
    {
        private string _fishKey;
        
        public FindFishStruggleTime(string fishKey)
        {
            _fishKey = fishKey;
        }
        
        protected override float OnDo()
        {
            var fishSystem = this.GetSystem<IFishSystem>();
            var struggleTime = fishSystem.FishInfos[_fishKey].StruggleTime;
            return struggleTime;
        }
    }
}