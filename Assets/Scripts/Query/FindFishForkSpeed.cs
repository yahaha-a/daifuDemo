using QFramework;

namespace daifuDemo
{
    public class FindFishForkSpeed : AbstractQuery<float>
    {
        private string _fishForkKey;

        private int _rank;
        
        public FindFishForkSpeed(string fishForkKey, int rank)
        {
            _fishForkKey = fishForkKey;
            _rank = rank;
        }
        
        protected override float OnDo()
        {
            var weaponSystem = this.GetSystem<IWeaponSystem>();
            var speed = weaponSystem.FishForkInfos[_fishForkKey][_rank].Speed;
            return speed;
        }
    }
}