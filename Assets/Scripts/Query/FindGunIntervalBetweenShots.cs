using QFramework;

namespace daifuDemo
{
    public class FindGunIntervalBetweenShots : AbstractQuery<float>
    {
        private string _gunKey;

        private int _rank;
        
        public FindGunIntervalBetweenShots(string gunKey, int rank)
        {
            _gunKey = gunKey;
            _rank = rank;
        }
        
        protected override float OnDo()
        {
            var weaponSystem = this.GetSystem<IWeaponSystem>();
            var gunIntervalBetweenShots = weaponSystem.GunInfos[(_gunKey, _rank)].IntervalBetweenShots;
            return gunIntervalBetweenShots;
        }
    }
}