using QFramework;

namespace daifuDemo
{
    public class FindFishClicks : AbstractQuery<int>
    {
        private string _fishKey;
        
        public FindFishClicks(string fishKey)
        {
            _fishKey = fishKey;
        }
        
        protected override int OnDo()
        {
            var fishSystem = this.GetSystem<IFishSystem>();
            var clicks = fishSystem.FishInfos[_fishKey].Clicks;
            return clicks;
        }
    }
}