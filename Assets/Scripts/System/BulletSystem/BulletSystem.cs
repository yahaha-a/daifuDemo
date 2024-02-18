using System.Collections.Generic;
using QFramework;

namespace daifuDemo
{
    public interface IBulletSystem : ISystem
    {
        Dictionary<string, Dictionary<BulletAttribute, Dictionary<int, IBulletInfo>>> BulletInfos { get; }

        IBulletSystem AddBulletInfo(string key, BulletAttribute bulletAttribute, int rank, IBulletInfo bulletInfo);
    }
    
    public class BulletSystem : AbstractSystem, IBulletSystem
    {
        public Dictionary<string, Dictionary<BulletAttribute, Dictionary<int, IBulletInfo>>> BulletInfos { get; } =
            new Dictionary<string, Dictionary<BulletAttribute, Dictionary<int, IBulletInfo>>>();
        
        protected override void OnInit()
        {
            this.AddBulletInfo(Config.RifleKey, BulletAttribute.Normal, 1, new BulletInfo()
                    .WithDamage(5f)
                    .WithSpeed(20f)
                    .WithRange(20f))
                .AddBulletInfo(Config.ShotgunKey, BulletAttribute.Normal, 1, new BulletInfo()
                    .WithDamage(4f)
                    .WithSpeed(25f)
                    .WithRange(10f));
        }

        public IBulletSystem AddBulletInfo(string key, BulletAttribute bulletAttribute, int rank, IBulletInfo bulletInfo)
        {
            BulletInfos.Add(key, new Dictionary<BulletAttribute, Dictionary<int, IBulletInfo>>()
            {
                {
                    bulletAttribute, new Dictionary<int, IBulletInfo>()
                    {
                        {
                            rank, bulletInfo
                        }
                    }
                }
            });
            return this;
        }
    }
}