using QFramework;

namespace daifuDemo
{
    public class FindFishSwimRate : AbstractQuery<float>
    {
        private string _fishKey;
        
        public FindFishSwimRate(string fishKey)
        {
            _fishKey = fishKey;
        }
        
        protected override float OnDo()
        {
            var fishSystem = this.GetSystem<IFishSystem>();
            var swimRate = fishSystem.FishInfos[_fishKey].SwimRate;
            return swimRate;
        }
    }
}