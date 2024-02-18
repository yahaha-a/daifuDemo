using QFramework;

namespace daifuDemo
{
    public class FindGunRotationRate : AbstractQuery<float>
    {
        private string _gunKey;

        private int _rank;
        
        public FindGunRotationRate(string gunKey, int rank)
        {
            _gunKey = gunKey;
            _rank = rank;
        }
        
        protected override float OnDo()
        {
            var weaponSystem = this.GetSystem<IWeaponSystem>();
            var gunRotationRate = weaponSystem.GunInfos[_gunKey][_rank].RotationRate;
            return gunRotationRate;
        }
    }
}