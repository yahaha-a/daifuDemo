using System.Collections.Generic;
using System.Linq;
using QFramework;
using UnityEngine;

namespace daifuDemo
{
    [System.Serializable]
    public class SerializableBackPackItemList
    {
        public List<SerializableKeyValuePair> Items;

        [System.Serializable]
        public class SerializableKeyValuePair
        {
            public string Key;
            public int Value;
        }
    }
    
    public interface IBackPackSystem : ISystem
    {
        Dictionary<string, IBackPackItemInfo> BackPackItemInfos { get; }
        
        Dictionary<string, int> ShipBackPackItemList { get; }
        
        Dictionary<string, int> SuShiBackPackItemList { get; }

        void AddBackPackItemList(string key, int count);

        string AccordingItemTypeGetRandomOne(BackPackItemType backPackItemType);

        void SaveData();

        void LoadData();
    }
    
    public class BackPackSystem : AbstractSystem, IBackPackSystem
    {
        private IMenuSystem _menuSystem;
        
        protected override void OnInit()
        {
            _menuSystem = this.GetSystem<IMenuSystem>();
            
            //TODO
            this.AddBackPackItemInfos(BackPackItemConfig.NormalFishPiecesKey, new BackPackItemInfo()
                    .WithItemKey(BackPackItemConfig.NormalFishPiecesKey)
                    .WithItemName("普通鱼块")
                    .WithItemIcon(null)
                    .WithItemType(BackPackItemType.Fish)
                    .WithItemDescription("肉质普通, 能吃"))
                .AddBackPackItemInfos(BackPackItemConfig.PteroisFishPiecesKey, new BackPackItemInfo()
                    .WithItemKey(BackPackItemConfig.PteroisFishPiecesKey)
                    .WithItemName("狮子鱼块")
                    .WithItemIcon(null)
                    .WithItemType(BackPackItemType.Fish)
                    .WithItemDescription("肉少刺多, 不太行"))
                .AddBackPackItemInfos(BackPackItemConfig.SaltKey, new BackPackItemInfo()
                    .WithItemKey(BackPackItemConfig.SaltKey)
                    .WithItemName("盐")
                    .WithItemIcon(null)
                    .WithItemType(BackPackItemType.Seasoning)
                    .WithItemDescription("烹饪中不可或缺的调味品, 注意适量使用"))
                .AddBackPackItemInfos(BackPackItemConfig.VinegarKey, new BackPackItemInfo()
                    .WithItemKey(BackPackItemConfig.VinegarKey)
                    .WithItemName("醋")
                    .WithItemIcon(null)
                    .WithItemType(BackPackItemType.Seasoning)
                    .WithItemDescription("常见调味品，由发酵的液体制成，具有酸味"))
                .AddBackPackItemInfos(BackPackItemConfig.KelpKey, new BackPackItemInfo()
                    .WithItemKey(BackPackItemConfig.KelpKey)
                    .WithItemName("海带")
                    .WithItemIcon(null)
                    .WithItemType(BackPackItemType.Ingredient)
                    .WithItemDescription("一种海藻，属于褐藻门，具有很高的营养价值"))
                .AddBackPackItemInfos(BackPackItemConfig.CoralKey, new BackPackItemInfo()
                    .WithItemKey(BackPackItemConfig.CoralKey)
                    .WithItemName("珊瑚")
                    .WithItemIcon(null)
                    .WithItemType(BackPackItemType.Ingredient)
                    .WithItemDescription("一种海洋无脊椎动物，它们以群体的形式生活，并形成珊瑚礁"))
                .AddBackPackItemInfos(BackPackItemConfig.CopperKey, new BackPackItemInfo()
                    .WithItemKey(BackPackItemConfig.CopperKey)
                    .WithItemName("铜矿石")
                    .WithItemIcon(null)
                    .WithItemType(BackPackItemType.Tool)
                    .WithItemDescription("重要的有色金属，具有良好的导电性和导热性"))
                .AddBackPackItemInfos(BackPackItemConfig.CordageKey, new BackPackItemInfo()
                    .WithItemKey(BackPackItemConfig.CordageKey)
                    .WithItemName("绳索")
                    .WithItemIcon(null)
                    .WithItemType(BackPackItemType.Tool)
                    .WithItemDescription("常见的道具, 升级武器需要"))
                .AddBackPackItemInfos(BackPackItemConfig.WoodKey, new BackPackItemInfo()
                    .WithItemKey(BackPackItemConfig.WoodKey)
                    .WithItemName("木头")
                    .WithItemIcon(null)
                    .WithItemType(BackPackItemType.Tool)
                    .WithItemDescription("常见的道具, 升级武器需要"));

            LoadData();
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
        
        public void SaveData()
        {
            SerializableBackPackItemList serializableItemList = new SerializableBackPackItemList
            {
                Items = ShipBackPackItemList.Select(kvp => new SerializableBackPackItemList.SerializableKeyValuePair
                {
                    Key = kvp.Key,
                    Value = kvp.Value
                }).ToList()
            };

            string shipBackPackItemJson = JsonUtility.ToJson(serializableItemList);
            PlayerPrefs.SetString("ShipBackPackItemList_data", shipBackPackItemJson);

            serializableItemList = new SerializableBackPackItemList
            {
                Items = SuShiBackPackItemList.Select(kvp => new SerializableBackPackItemList.SerializableKeyValuePair
                {
                    Key = kvp.Key,
                    Value = kvp.Value
                }).ToList()
            };

            string sushiBackPackItemJson = JsonUtility.ToJson(serializableItemList);
            PlayerPrefs.SetString("sushiBackPackItemList_data", sushiBackPackItemJson);
            PlayerPrefs.Save();
        }

        public void LoadData()
        {
            string shipbackPackItemJson = PlayerPrefs.GetString("ShipBackPackItemList_data", "");

            SerializableBackPackItemList loadedItemList =
                JsonUtility.FromJson<SerializableBackPackItemList>(shipbackPackItemJson);

            ShipBackPackItemList.Clear();

            if (loadedItemList != null && loadedItemList.Items != null)
            {
                foreach (var kvp in loadedItemList.Items)
                {
                    ShipBackPackItemList.Add(kvp.Key, kvp.Value);
                }
            }

            string sushiPackItemJson = PlayerPrefs.GetString("sushiBackPackItemList_data", "");

            loadedItemList = JsonUtility.FromJson<SerializableBackPackItemList>(sushiPackItemJson);
            SuShiBackPackItemList.Clear();

            if (loadedItemList != null && loadedItemList.Items != null)
            {
                foreach (var kvp in loadedItemList.Items)
                {
                    SuShiBackPackItemList.Add(kvp.Key, kvp.Value);
                }
            }
        }
    }
}