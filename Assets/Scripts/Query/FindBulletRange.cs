using QFramework;

namespace daifuDemo
{
    public class FindBulletRange : AbstractQuery<float>
    {
        private string _gunKey;

        private BulletAttribute _bulletAttribute;

        private int _rank;
        
        public FindBulletRange(string gunKey, BulletAttribute bulletAttribute, int rank)
        {
            _gunKey = gunKey;
            _bulletAttribute = bulletAttribute;
            _rank = rank;
        }
        
        protected override float OnDo()
        {
            var bulletSystem = this.GetSystem<IBulletSystem>();
            var bulletRange = bulletSystem.BulletInfos[_gunKey][_bulletAttribute][_rank].Range;
            return bulletRange;
        }
    }
}