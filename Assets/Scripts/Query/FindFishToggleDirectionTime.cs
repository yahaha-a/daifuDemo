using QFramework;

namespace daifuDemo
{
    public class FindFishToggleDirectionTime : AbstractQuery<float>
    {
        private string _fishKey;
        
        public FindFishToggleDirectionTime(string fishKey)
        {
            _fishKey = fishKey;
        }
        
        protected override float OnDo()
        {
            var fishSystem = this.GetSystem<IFishSystem>();
            var toggleDirectionTime = fishSystem.FishInfos[_fishKey].ToggleDirectionTime;
            return toggleDirectionTime;
        }
    }
}