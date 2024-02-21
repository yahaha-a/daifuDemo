using QFramework;

namespace daifuDemo
{
    public class FindFishForkRotationRate : AbstractQuery<float>
    {
        private string _fishForkKey;

        private int _rank;
        
        public FindFishForkRotationRate(string fishForkKey, int rank)
        {
            _fishForkKey = fishForkKey;
            _rank = rank;
        }
        
        protected override float OnDo()
        {
            var weaponSystem = this.GetSystem<IWeaponSystem>();
            var rotationRate = weaponSystem.FishForkInfos[(_fishForkKey, _rank)].RotationRate;
            return rotationRate;
        }
    }
}