using QFramework;

namespace daifuDemo
{
    public class FindFishState : AbstractQuery<FishState>
    {
        private string _fishKey;
        
        public FindFishState(string fishKey)
        {
            _fishKey = fishKey;
        }
        
        protected override FishState OnDo()
        {
            var fishSystem = this.GetSystem<FishSystem>();
            var fishState = fishSystem.FishInfos[_fishKey].FishState;
            return fishState;
        }
    }
}