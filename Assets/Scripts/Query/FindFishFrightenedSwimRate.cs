using QFramework;

namespace daifuDemo
{
    public class FindFishFrightenedSwimRate : AbstractQuery<float>
    {
        private string _fishKey;
        
        public FindFishFrightenedSwimRate(string fishKey)
        {
            _fishKey = fishKey;
        }
        
        protected override float OnDo()
        {
            var fishSystem = this.GetSystem<IFishSystem>();
            var frightenedSwimRate = fishSystem.FishInfos[_fishKey].FrightenedSwimRate;
            return frightenedSwimRate;
        }
    }
}