using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public interface IFishSystem : ISystem
    {
        Dictionary<string, IFishInfo> FishInfos { get; }
    }
    
    public class FishSystem : AbstractSystem, IFishSystem
    {
        private static ResLoader _resLoader = ResLoader.Allocate();

        public Dictionary<string, IFishInfo> FishInfos { get; } = new Dictionary<string, IFishInfo>()
        {
            {Config.NormalFishKey, new FishInfo()
                .WithFishName("普通鱼")
                .WithFishKey(Config.NormalFishKey)
                .WithFishPrefab(_resLoader.LoadSync<GameObject>("NormalFish"))
                .WithFishState(FishState.Swim)
                .WithSwimRate(3f)
                .WithFrightenedSwimRate(5f)
                .WithToggleDirectionTime(5f)
                .WithRangeOfMovement(10f)
                .WithHp(20f)
            },
            {Config.PteroisKey, new AggressiveFishInfo()
                .WithDamage(5f)
                .WithPursuitSwimRate(5f)
                .WithSwimRate(5f)
                .WithFishName("狮子鱼")
                .WithFishKey(Config.PteroisKey)
                .WithFishPrefab(_resLoader.LoadSync<GameObject>("Pterois"))
                .WithFishState(FishState.Swim)
                .WithSwimRate(4f)
                .WithFrightenedSwimRate(6f)
                .WithToggleDirectionTime(3f)
                .WithRangeOfMovement(5f)
                .WithHp(10f)
            },
        };
        
        protected override void OnInit()
        {
            Events.WeaponAttackFish.Register((damage, fish) =>
            {
                fish.GetComponent<IFish>().Hp -= damage;
                Debug.Log(fish.GetComponent<IFish>().Hp);
                if (Mathf.Approximately(fish.GetComponent<IFish>().Hp, 0))
                {
                    fish.DestroySelf();
                }
            });
        }
        
        private Dictionary<string, IFishInfo> Add(string key, IFishInfo fishInfo)
        {
            FishInfos.Add(key, fishInfo);
            return FishInfos;
        }
    }
}