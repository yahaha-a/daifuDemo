using QFramework;

namespace daifuDemo
{
    public class FindFishFleeHp : AbstractQuery<float>
    {
        private string _fishKey;
        
        public FindFishFleeHp(string fishKey)
        {
            _fishKey = fishKey;
        }
        
        protected override float OnDo()
        {
            var fishSystem = this.GetSystem<IFishSystem>();
            var fleeHp = fishSystem.FishInfos[_fishKey].FleeHp;
            return fleeHp;
        }
    }
}