using QFramework;

namespace daifuDemo
{
    public class FindBulletSpeed : AbstractQuery<float>
    {
        private string _gunKey;

        private BulletAttribute _bulletAttribute;

        private int _rank;
        
        public FindBulletSpeed(string gunKey, BulletAttribute bulletAttribute, int rank)
        {
            _gunKey = gunKey;
            _bulletAttribute = bulletAttribute;
            _rank = rank;
        }
        
        protected override float OnDo()
        {
            var bulletSystem = this.GetSystem<IBulletSystem>();
            var bulletSpeed = bulletSystem.BulletInfos[_gunKey][_bulletAttribute][_rank].Speed;
            return bulletSpeed;
        }
    }
}