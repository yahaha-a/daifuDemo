using QFramework;

namespace daifuDemo
{
    public class FindFishVisualField : AbstractQuery<float>
    {
        private string _fishKey;
        
        public FindFishVisualField(string fishKey)
        {
            _fishKey = fishKey;
        }
        
        protected override float OnDo()
        {
            var fishSystem = this.GetSystem<IFishSystem>();
            var visualField = fishSystem.FishInfos[_fishKey].VisualField;
            return visualField;
        }
    }
}