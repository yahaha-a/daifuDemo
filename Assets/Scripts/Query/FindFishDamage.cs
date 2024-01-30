using QFramework;

namespace daifuDemo
{
    public class FindFishDamage : AbstractQuery<float>
    {
        private string _fishKey;
        
        public FindFishDamage(string fishKey)
        {
            _fishKey = fishKey;
        }
        
        protected override float OnDo()
        {
            var fishSystem = this.GetSystem<IFishSystem>();
            var fishInfo = fishSystem.FishInfos[_fishKey] as AggressiveFishInfo;
            var damage = fishInfo.Damage;
            return damage;
        }
    }
}