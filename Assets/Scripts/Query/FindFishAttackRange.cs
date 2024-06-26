using QFramework;

namespace daifuDemo
{
    public class FindFishAttackRange : AbstractQuery<float>
    {
        private string _fishKey;
        
        public FindFishAttackRange(string fishKey)
        {
            _fishKey = fishKey;
        }
        
        protected override float OnDo()
        {
            var fishSystem = this.GetSystem<IFishSystem>();
            var fishInfo = fishSystem.FishInfos[_fishKey] as AggressiveFishInfo;
            var attackRange = fishInfo.AttackRange;
            return attackRange;
        }
    }
}