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
                .WithFishKey("normal_fish")
                .WithFishPrefab(_resLoader.LoadSync<GameObject>("NormalFish"))
                .WithFishState(FishState.Swim)
                .WithSwimRate(3f)
                .WithFrightenedSwimRate(5f)
                .WithToggleDirectionTime(5f)
                .WithRangeOfMovement(10f)}
        };
        
        protected override void OnInit()
        {
            
        }
        
        private Dictionary<string, IFishInfo> Add(string key, IFishInfo fishInfo)
        {
            FishInfos.Add(key, fishInfo);
            return FishInfos;
        }
    }
}