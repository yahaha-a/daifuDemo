using QFramework;

namespace daifuDemo
{
    public class FindFishPursuitSwimRate : AbstractQuery<float>
    {
        private string _fishKey;
        
        public FindFishPursuitSwimRate(string fishKey)
        {
            _fishKey = fishKey;
        }
        
        protected override float OnDo()
        {
            var fishSystem = this.GetSystem<IFishSystem>();
            var fishInfo = fishSystem.FishInfos[_fishKey] as AggressiveFishInfo;
            var pursuitSwimRate = fishInfo.PursuitSwimRate;
            return pursuitSwimRate;
        }
    }
}