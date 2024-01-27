using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public interface IFishSystem : ISystem
    {
        EasyEvent FishInfosAddComplete { get; }
        
        Dictionary<string, IFishInfo> FishInfos { get; }
    }
    
    public class FishSystem : AbstractSystem, IFishSystem
    {
        public EasyEvent FishInfosAddComplete { get; } = new EasyEvent();
        
        private ResLoader _resLoader = ResLoader.Allocate();
        
        public Dictionary<string, IFishInfo> FishInfos { get; } = new Dictionary<string, IFishInfo>();
        
        protected override void OnInit()
        {
            Add(this.GetModel<IFishMode>().NormalFishKey,
                new FishInfo()
                    .WithFishName("普通鱼")
                    .WithFishPrefab(_resLoader.LoadSync<GameObject>("NormalFish"))
                    .WithFishState(FishState.Swim)
                    .WithSwimRate(3f)
                    .WithToggleDirectionTime(5f));
            
            FishInfosAddComplete.Trigger();
        }
        
        private Dictionary<string, IFishInfo> Add(string key, IFishInfo fishInfo)
        {
            FishInfos.Add(key, fishInfo);
            return FishInfos;
        }
    }
}