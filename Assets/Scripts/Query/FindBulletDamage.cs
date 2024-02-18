using QFramework;

namespace daifuDemo
{
    public class FindBulletDamage : AbstractQuery<float>
    {
        private string _gunKey;

        private BulletAttribute _bulletAttribute;

        private int _rank;
        
        public FindBulletDamage(string gunKey, BulletAttribute bulletAttribute, int rank)
        {
            _gunKey = gunKey;
            _bulletAttribute = bulletAttribute;
            _rank = rank;
        }
        
        protected override float OnDo()
        {
            var bulletSystem = this.GetSystem<IBulletSystem>();
            var bulletDamage = bulletSystem.BulletInfos[_gunKey][_bulletAttribute][_rank].Damage;
            return bulletDamage;
        }
    }
}