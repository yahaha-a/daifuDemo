using System.Collections.Generic;
using System.Linq;
using Global;
using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public interface IBackPackSystem : ISystem
    {
        Dictionary<string, IBackPackItemInfo> BackPackItemInfos { get; }
        
        Dictionary<string, int> ShipBackPackItemList { get; }
        
        Dictionary<string, int> SuShiBackPackItemList { get; }

        void AddBackPackItemList(string key, int count);

        string AccordingItemTypeGetRandomOne(BackPackItemType backPackItemType);
    }
    
    public class BackPackSystem : AbstractSystem, IBackPackSystem
    {
        private static ResLoader _resLoader = ResLoader.Allocate();
        
        private IMenuSystem _menuSystem;

        private IArchiveSystem _archiveSystem;
        
        protected override void OnInit()
        {
            _menuSystem = this.GetSystem<IMenuSystem>();
            _archiveSystem = this.GetSystem<IArchiveSystem>();
            
            //TODO
            this.AddBackPackItemInfos(BackPackItemConfig.NormalFishPiecesKey, new BackPackItemInfo()
                    .WithItemKey(BackPackItemConfig.NormalFishPiecesKey)
                    .WithItemName("普通鱼块")
                    .WithItemIcon(_resLoader.LoadSync<Sprite>(Config.NormalFishKey))
                    .WithItemType(BackPackItemType.Fish)
                    .WithItemDescription("肉质普通, 能吃"))
                .AddBackPackItemInfos(BackPackItemConfig.PteroisFishPiecesKey, new BackPackItemInfo()
                    .WithItemKey(BackPackItemConfig.PteroisFishPiecesKey)
                    .WithItemName("狮子鱼块")
                    .WithItemIcon(_resLoader.LoadSync<Sprite>(Config.AggressiveFishKey))
                    .WithItemType(BackPackItemType.Fish)
                    .WithItemDescription("肉少刺多, 不太行"))
                .AddBackPackItemInfos(BackPackItemConfig.SaltKey, new BackPackItemInfo()
                    .WithItemKey(BackPackItemConfig.SaltKey)
                    .WithItemName("盐")
                    .WithItemIcon(_resLoader.LoadSync<Sprite>(BackPackItemConfig.SaltKey))
                    .WithItemType(BackPackItemType.Seasoning)
                    .WithItemDescription("烹饪中不可或缺的调味品, 注意适量使用"))
                .AddBackPackItemInfos(BackPackItemConfig.VinegarKey, new BackPackItemInfo()
                    .WithItemKey(BackPackItemConfig.VinegarKey)
                    .WithItemName("醋")
                    .WithItemIcon(_resLoader.LoadSync<Sprite>(BackPackItemConfig.VinegarKey))
                    .WithItemType(BackPackItemType.Seasoning)
                    .WithItemDescription("常见调味品，由发酵的液体制成，具有酸味"))
                .AddBackPackItemInfos(BackPackItemConfig.KelpKey, new BackPackItemInfo()
                    .WithItemKey(BackPackItemConfig.KelpKey)
                    .WithItemName("海带")
                    .WithItemIcon(_resLoader.LoadSync<Sprite>(BackPackItemConfig.KelpKey))
                    .WithItemType(BackPackItemType.Ingredient)
                    .WithItemDescription("一种海藻，属于褐藻门，具有很高的营养价值"))
                .AddBackPackItemInfos(BackPackItemConfig.CoralKey, new BackPackItemInfo()
                    .WithItemKey(BackPackItemConfig.CoralKey)
                    .WithItemName("珊瑚")
                    .WithItemIcon(_resLoader.LoadSync<Sprite>(BackPackItemConfig.CoralKey))
                    .WithItemType(BackPackItemType.Ingredient)
                    .WithItemDescription("一种海洋无脊椎动物，它们以群体的形式生活，并形成珊瑚礁"))
                .AddBackPackItemInfos(BackPackItemConfig.CopperKey, new BackPackItemInfo()
                    .WithItemKey(BackPackItemConfig.CopperKey)
                    .WithItemName("铜矿石")
                    .WithItemIcon(_resLoader.LoadSync<Sprite>(BackPackItemConfig.CopperKey))
                    .WithItemType(BackPackItemType.Tool)
                    .WithItemDescription("重要的有色金属，具有良好的导电性和导热性"))
                .AddBackPackItemInfos(BackPackItemConfig.CordageKey, new BackPackItemInfo()
                    .WithItemKey(BackPackItemConfig.CordageKey)
                    .WithItemName("绳索")
                    .WithItemIcon(_resLoader.LoadSync<Sprite>(BackPackItemConfig.CordageKey))
                    .WithItemType(BackPackItemType.Tool)
                    .WithItemDescription("常见的道具, 升级武器需要"))
                .AddBackPackItemInfos(BackPackItemConfig.WoodKey, new BackPackItemInfo()
                    .WithItemKey(BackPackItemConfig.WoodKey)
                    .WithItemName("木头")
                    .WithItemIcon(_resLoader.LoadSync<Sprite>(BackPackItemConfig.WoodKey))
                    .WithItemType(BackPackItemType.Tool)
                    .WithItemDescription("常见的道具, 升级武器需要"));

            Events.GameStart.Register(() =>
            {
                _archiveSystem.LoadData(ShipBackPackItemList, "ShipBackPackItemList");
                _archiveSystem.LoadData(SuShiBackPackItemList, "SuShiBackPackItemList");
            });
        }

        public Dictionary<string, IBackPackItemInfo> BackPackItemInfos { get; } =
            new Dictionary<string, IBackPackItemInfo>();

        public Dictionary<string, int> ShipBackPackItemList { get; } = new Dictionary<string, int>();

        public Dictionary<string, int> SuShiBackPackItemList { get; } = new Dictionary<string, int>();

        private BackPackSystem AddBackPackItemInfos(string key, IBackPackItemInfo backPackItemInfo)
        {
            BackPackItemInfos.Add(key, backPackItemInfo);
            return this;
        }

        public void AddBackPackItemList(string key, int count)
        {
            if (BackPackItemInfos[key].ItemType == BackPackItemType.Tool ||
                BackPackItemInfos[key].ItemType == BackPackItemType.Weapon)
            {
                if (ShipBackPackItemList.ContainsKey(key))
                {
                    ShipBackPackItemList[key] += count;
                }
                else
                {
                    ShipBackPackItemList.Add(key, count);
                }
            }
            else
            {
                if (SuShiBackPackItemList.ContainsKey(key))
                {
                    SuShiBackPackItemList[key] += count;
                }
                else
                {
                    SuShiBackPackItemList.Add(key, count);
                }
            }
        }

        public string AccordingItemTypeGetRandomOne(BackPackItemType backPackItemType)
        {
            if (backPackItemType == BackPackItemType.WeaponLevel)
            {
                return null;
            }
            
            List<string> itemList = new List<string>();
            foreach (var (key, backPackItemInfo) in BackPackItemInfos)
            {
                if (backPackItemInfo.ItemType == backPackItemType)
                {
                    itemList.Add(key);
                }
            }

            return itemList[Random.Range(0, itemList.Count)];
        }
    }
}