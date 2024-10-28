using QFramework;

namespace daifuDemo
{
    public class FindFishChargeTime : AbstractQuery<float>
    {
        private string _fishKey;

        public FindFishChargeTime(string fishKey)
        {
            _fishKey = fishKey;
        }
        
        protected override float OnDo()
        {
            var fishSystem = this.GetSystem<IFishSystem>();
            var fishInfo = fishSystem.FishInfos[_fishKey] as AggressiveFishInfo;
            var chargeTime = fishInfo.ChargeTime;
            return chargeTime;
        }
    }
}