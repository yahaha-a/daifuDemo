using System.Collections.Generic;
using Global;
using QFramework;

namespace daifuDemo
{
    public interface IBulletSystem : ISystem
    {
        Dictionary<string, List<IBulletInfo>> BulletInfos { get; }
    }
    
    public class BulletSystem : AbstractSystem, IBulletSystem
    {
        public Dictionary<string, List<IBulletInfo>> BulletInfos { get; } = new Dictionary<string, List<IBulletInfo>>();

        private IGunModel _gunModel;
        
        private IBulletModel _bulletModel;

        protected override void OnInit()
        {
            //TODO
            this.AddBulletInfo(Config.RifleKey, new BulletInfo()
                    .WithType(BulletType.Normal)
                    .WithName("普通步枪子弹")
                    .WithDamage(5f)
                    .WithPrice(10f))
                .AddBulletInfo(Config.RifleKey, new BulletInfo()
                    .WithType(BulletType.Hypnosis)
                    .WithName("催眠步枪子弹")
                    .WithDamage(4f)
                    .WithPrice(20f))
                .AddBulletInfo(Config.ShotgunKey, new BulletInfo()
                    .WithType(BulletType.Normal)
                    .WithName("普通霞弹枪子弹")
                    .WithDamage(3f)
                    .WithPrice(5f))
                .AddBulletInfo(Config.ShotgunKey, new BulletInfo()
                    .WithType(BulletType.Hypnosis)
                    .WithName("催眠霞弹枪子弹")
                    .WithDamage(2f)
                    .WithPrice(10f));

            _gunModel = this.GetModel<IGunModel>();
            _bulletModel = this.GetModel<IBulletModel>();
        }

        public IBulletInfo GetBulletInfo(string key, BulletType type)
        {
            return BulletInfos[key].Find(info => info.Type == type);
        }

        private BulletSystem AddBulletInfo(string key, IBulletInfo bulletInfo)
        {
            if (!BulletInfos.ContainsKey(key))
            {
                BulletInfos.Add(key, new List<IBulletInfo>());
            }
            
            BulletInfos[key].Add(bulletInfo);

            return this;
        }
    }
}