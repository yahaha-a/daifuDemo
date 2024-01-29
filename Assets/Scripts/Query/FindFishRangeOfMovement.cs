using QFramework;

namespace daifuDemo
{
    public class FindFishRangeOfMovement : AbstractQuery<float>
    {
        private string _fishKey;
        
        public FindFishRangeOfMovement(string fishKey)
        {
            _fishKey = fishKey;
        }
        
        protected override float OnDo()
        {
            var fishSystem = this.GetSystem<IFishSystem>();
            var rangeOfMovement = fishSystem.FishInfos[_fishKey].RangeOfMovement;
            return rangeOfMovement;
        }
    }
}