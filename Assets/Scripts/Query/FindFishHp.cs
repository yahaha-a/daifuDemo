using QFramework;

namespace daifuDemo
{
    public class FindFishHp : AbstractQuery<float>
    {
        private string _fishKey;
        
        public FindFishHp(string fishKey)
        {
            _fishKey = fishKey;
        }
        
        protected override float OnDo()
        {
            var fishSystem = this.GetSystem<IFishSystem>();
            var hp = fishSystem.FishInfos[_fishKey].Hp;
            return hp;
        }
    }
}