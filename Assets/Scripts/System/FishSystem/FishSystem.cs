using System.Collections.Generic;
using Global;
using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public interface IFishSystem : ISystem
    {
        Dictionary<string, IFishInfo> FishInfos { get; }
        
        Dictionary<string, ICaughtItemInfo> CaughtItem { get; }

        void Reload();
    }
    
    public class FishSystem : AbstractSystem, IFishSystem
    {
        private static ResLoader _resLoader = ResLoader.Allocate();

        private IBackPackSystem _backPackSystem;

        private ITreasureBoxSystem _treasureBoxSystem;

        private IPlayerModel _playerModel;

        private IUtils _utils;
        
        //TODO
        public Dictionary<string, IFishInfo> FishInfos { get; } = new Dictionary<string, IFishInfo>()
        {
            {Config.NormalFishKey, new FishInfo()
                .WithFishName("普通鱼")
                .WithFishKey(Config.NormalFishKey)
                .WithFishIcon(_resLoader.LoadSync<Texture2D>(Config.NormalFishKey))
                .WithFishPrefab(_resLoader.LoadSync<GameObject>(Config.NormalFishKey))
                .WithFishState(FishState.Swim)
                .WithSwimRate(2f)
                .WithFrightenedSwimRate(5f)
                .WithCoolDownTime(4f)
                .WithToggleDirectionTime(4f)
                .WithHp(20f)
                .WithStruggleTime(2f)
                .WithVisualField(4f)
                .WithClicks(5)
            },
            {Config.AggressiveFishKey, new AggressiveFishInfo()
                .WithAttackInterval(3f)
                .WithAttackRange(3f)
                .WithDamage(5f)
                .WithPursuitSwimRate(5f)
                .WithChargeTime(0.2f)
                .WithFishName("狮子鱼")
                .WithFishKey(Config.AggressiveFishKey)
                .WithFishIcon(_resLoader.LoadSync<Texture2D>(Config.AggressiveFishKey))
                .WithFishPrefab(_resLoader.LoadSync<GameObject>(Config.AggressiveFishKey))
                .WithFishState(FishState.Swim)
                .WithSwimRate(2f)
                .WithFrightenedSwimRate(8f)
                .WithCoolDownTime(3f)
                .WithToggleDirectionTime(3f)
                .WithHp(30f)
                .WithFleeHp(15f)
                .WithStruggleTime(1.5f)
                .WithVisualField(5f)
                .WithClicks(6)
            },
        };

        public Dictionary<string, ICaughtItemInfo> CaughtItem { get; } = new Dictionary<string, ICaughtItemInfo>();


        protected override void OnInit()
        {
            _backPackSystem = this.GetSystem<IBackPackSystem>();
            _treasureBoxSystem = this.GetSystem<ITreasureBoxSystem>();
            _playerModel = this.GetModel<IPlayerModel>();
            _utils = this.GetUtility<IUtils>();
            
            Events.WeaponAttackFish.Register((damage, fish) =>
            {
                var fishMessage = fish.GetComponent<IFish>();
                fishMessage.Hp -= damage;
                fishMessage.HitByBullet = true;
                if (Mathf.Approximately(fishMessage.Hp, 0))
                {
                    fish.DestroySelf();
                    
                    if (CaughtItem.ContainsKey(fishMessage.FishKey + "_Star" + 1) && CaughtItem[fishMessage.FishKey + "_Star" + 1].Star == 1)
                    {
                        CaughtItem[fishMessage.FishKey + "_Star" + 1].Amount += 1;
                    }
                    else
                    {
                        CaughtItem.Add(fishMessage.FishKey + "_Star" + 1, new CaughtItemInfo()
                            .WithFishKey(fishMessage.FishKey)
                            .WithFishName(FishInfos[fishMessage.FishKey].FishName)
                            .WithFishIcon(_utils.AdjustSprite(FishInfos[fishMessage.FishKey].FishIcon))
                            .WithStar(1)
                            .WithAmount(1)
                        );
                        
                    }
                    
                    Events.ObtainItem?.Trigger(new ObtainItemsInfo()
                        .WithName("1星" + FishInfos[fishMessage.FishKey].FishName)
                        .WithIconKey(FishInfos[fishMessage.FishKey].FishKey)
                        .WithNumber(1)
                        .WithStartPosition(fish.transform.position));
                }
            });

            Events.HitFish.Register(fish =>
            {
                fish.GetComponent<IFish>().HitByFork = true;
                fish.GetComponent<IFish>().HitPosition = fish.gameObject.transform.position;
            });

            Events.CatchFish.Register(fish =>
            {
                if (CaughtItem.ContainsKey(fish.FishKey + "_Star" + 3) && CaughtItem[fish.FishKey + "_Star" + 3].Star == 3)
                {
                    CaughtItem[fish.FishKey + "_Star" + 3].Amount += 1;
                }
                else
                {
                    CaughtItem.Add(fish.FishKey + "_Star" + 3, new CaughtItemInfo()
                        .WithFishKey(fish.FishKey)
                        .WithFishName(FishInfos[fish.FishKey].FishName)
                        .WithFishIcon(_utils.AdjustSprite(FishInfos[fish.FishKey].FishIcon))
                        .WithStar(3)
                        .WithAmount(1)
                    );
                }
                
                Events.ObtainItem?.Trigger(new ObtainItemsInfo()
                    .WithName("3星" + FishInfos[fish.FishKey].FishName)
                    .WithIconKey(FishInfos[fish.FishKey].FishKey)
                    .WithNumber(1)
                    .WithStartPosition(_playerModel.CurrentPosition.Value));
            });

            Events.TreasureBoxOpened.Register(treasure =>
            {
                if (CaughtItem.ContainsKey(treasure.backPackItemKey))
                {
                    CaughtItem[treasure.backPackItemKey].Amount += 1;
                }
                else
                {
                    CaughtItem.Add(treasure.backPackItemKey, new CaughtItemInfo()
                        .WithFishKey(treasure.backPackItemKey)
                        .WithFishName(_backPackSystem.BackPackItemInfos[treasure.backPackItemKey].ItemName)
                        .WithFishIcon(_backPackSystem.BackPackItemInfos[treasure.backPackItemKey].ItemIcon)
                        .WithStar(0)
                        .WithAmount(1));
                }
                
                Events.ObtainItem?.Trigger(new ObtainItemsInfo()
                    .WithName(_backPackSystem.BackPackItemInfos[treasure.backPackItemKey].ItemName)
                    .WithIconKey(_backPackSystem.BackPackItemInfos[treasure.backPackItemKey].ItemKey)
                    .WithNumber(_treasureBoxSystem.TreasureItemInfos[treasure.key].Number)
                    .WithStartPosition(_playerModel.CurrentPosition.Value));
            });

            Events.ItemPickUped.Register(item =>
            {
                if (CaughtItem.ContainsKey(item.key))
                {
                    CaughtItem[item.key].Amount += 1;
                }
                else
                {
                    CaughtItem.Add(item.key, new CaughtItemInfo()
                        .WithFishKey(item.key)
                        .WithFishName(_backPackSystem.BackPackItemInfos[item.key].ItemName)
                        .WithFishIcon(_backPackSystem.BackPackItemInfos[item.key].ItemIcon)
                        .WithStar(0)
                        .WithAmount(1));
                }
                
                Events.ObtainItem?.Trigger(new ObtainItemsInfo()
                    .WithName(_backPackSystem.BackPackItemInfos[item.key].ItemName)
                    .WithIconKey(_backPackSystem.BackPackItemInfos[item.key].ItemKey)
                    .WithNumber(1)
                    .WithStartPosition(item.gameObject.transform.position));
            });
        }

        public void Reload()
        {
            CaughtItem.Clear();
        }
    }
}