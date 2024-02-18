using System.Collections.Generic;
using QFramework;

namespace daifuDemo
{
    public interface IBulletSystem : ISystem
    {
        Dictionary<string, Dictionary<BulletAttribute, Dictionary<int, IBulletInfo>>> BulletInfos { get; }
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
                .WithRange(20f));
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