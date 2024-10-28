using QFramework;

namespace daifuDemo
{
    public class FindFishCoolDownTime : AbstractQuery<float>
    {
        private string _fishKey;
        
        public FindFishCoolDownTime(string fishKey)
        {
            _fishKey = fishKey;
        }
        
        protected override float OnDo()
        {
            var fishSystem = this.GetSystem<IFishSystem>();
            var coolDownTime = fishSystem.FishInfos[_fishKey].CoolDownTime;
            return coolDownTime;
        }
    }
}