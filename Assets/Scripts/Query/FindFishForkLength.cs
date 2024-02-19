using QFramework;

namespace daifuDemo
{
    public class FindFishForkLength : AbstractQuery<float>
    {
        private string _fishForkKey;

        private int _rank;
        
        public FindFishForkLength(string fishForkKey, int rank)
        {
            _fishForkKey = fishForkKey;
            _rank = rank;
        }
        
        protected override float OnDo()
        {
            var weaponSystem = this.GetSystem<IWeaponSystem>();
            var fishForkLength = weaponSystem.FishForkInfos[_fishForkKey][_rank].FishForkLength;
            return fishForkLength;
        }
    }
}