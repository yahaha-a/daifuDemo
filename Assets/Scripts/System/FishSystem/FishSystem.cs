using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public interface IFishSystem : ISystem
    {
        Dictionary<string, IFishInfo> FishInfos { get; }
        
        Dictionary<string, ICaughtFishInfo> CaughtFish { get; }

        void Reload();
    }
    
    public class FishSystem : AbstractSystem, IFishSystem
    {
        private static ResLoader _resLoader = ResLoader.Allocate();

        public Dictionary<string, IFishInfo> FishInfos { get; } = new Dictionary<string, IFishInfo>()
        {
            {Config.NormalFishKey, new FishInfo()
                .WithFishName("普通鱼")
                .WithFishKey(Config.NormalFishKey)
                .WithFishIcon(_resLoader.LoadSync<Sprite>(Config.NormalFishIcon))
                .WithFishPrefab(_resLoader.LoadSync<GameObject>("NormalFish"))
                .WithFishState(FishState.Swim)
                .WithSwimRate(3f)
                .WithFrightenedSwimRate(5f)
                .WithToggleDirectionTime(5f)
                .WithRangeOfMovement(10f)
                .WithHp(20f)
            },
            {Config.PteroisKey, new AggressiveFishInfo()
                .WithAttackInterval(2f)
                .WithDamage(5f)
                .WithPursuitSwimRate(5f)
                .WithSwimRate(5f)
                .WithFishName("狮子鱼")
                .WithFishKey(Config.PteroisKey)
                .WithFishIcon(_resLoader.LoadSync<Sprite>(Config.PteroisIcon))
                .WithFishPrefab(_resLoader.LoadSync<GameObject>("Pterois"))
                .WithFishState(FishState.Swim)
                .WithSwimRate(4f)
                .WithFrightenedSwimRate(6f)
                .WithToggleDirectionTime(3f)
                .WithRangeOfMovement(5f)
                .WithHp(10f)
            },
        };

        public Dictionary<string, ICaughtFishInfo> CaughtFish { get; } = new Dictionary<string, ICaughtFishInfo>();


        protected override void OnInit()
        {
            Events.WeaponAttackFish.Register((damage, fish) =>
            {
                var fishMessage = fish.GetComponent<IFish>();
                fishMessage.Hp -= damage;
                if (Mathf.Approximately(fishMessage.Hp, 0))
                {
                    fish.DestroySelf();
                    
                    if (CaughtFish.ContainsKey(fishMessage.FishKey + "_Star" + 1) && CaughtFish[fishMessage.FishKey + "_Star" + 1].Star == 1)
                    {
                        CaughtFish[fishMessage.FishKey + "_Star" + 1].Amount += 1;
                    }
                    else
                    {
                        CaughtFish.Add(fishMessage.FishKey + "_Star" + 1, new CaughtFishInfo()
                            .WithFishKey(fishMessage.FishKey)
                            .WithFishName(FishInfos[fishMessage.FishKey].FishName)
                            .WithFishIcon(FishInfos[fishMessage.FishKey].FishIcon)
                            .WithStar(1)
                            .WithAmount(1)
                        );
                    }
                }
            });

            Events.HitFish.Register(fish =>
            {
                fish.GetComponent<IFish>().HitByFishFork();
            });

            Events.CatchFish.Register(fish =>
            {
                if (CaughtFish.ContainsKey(fish.FishKey + "_Star" + 3) && CaughtFish[fish.FishKey + "_Star" + 3].Star == 3)
                {
                    CaughtFish[fish.FishKey + "_Star" + 3].Amount += 1;
                }
                else
                {
                    CaughtFish.Add(fish.FishKey + "_Star" + 3, new CaughtFishInfo()
                        .WithFishKey(fish.FishKey)
                        .WithFishName(FishInfos[fish.FishKey].FishName)
                        .WithFishIcon(FishInfos[fish.FishKey].FishIcon)
                        .WithStar(3)
                        .WithAmount(1)
                    );
                }
            });
        }

        public void Reload()
        {
            CaughtFish.Clear();
        }
    }
}