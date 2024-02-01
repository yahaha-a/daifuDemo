using QFramework;

namespace daifuDemo
{
    public class FindFishAttackInterval : AbstractQuery<float>
    {
        private string _fishKey;
        
        public FindFishAttackInterval(string fishKey)
        {
            _fishKey = fishKey;
        }
        
        protected override float OnDo()
        {
            var fishSystem = this.GetSystem<IFishSystem>();
            var fishInfo = fishSystem.FishInfos[_fishKey] as AggressiveFishInfo;
            var attackInterval = fishInfo.AttackInterval;
            return attackInterval;
        }
    }
}