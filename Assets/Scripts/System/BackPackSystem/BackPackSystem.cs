using System.Collections.Generic;
using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public interface IBackPackSystem : ISystem
    {
        Dictionary<string, IBackPackItemInfo> BackPackItemInfos { get; }
        
        Dictionary<string, int> BackPackItemList { get; }

        IBackPackSystem AddBackPackItemInfos(string key, IBackPackItemInfo backPackItemInfo);

        void AddBackPackItemList(string key, int count);

        void SaveData();

        void LoadData();
    }
    
    public class BackPackSystem : AbstractSystem, IBackPackSystem
    {
        protected override void OnInit()
        {
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
                    .WithItemDescription("肉少刺多, 不太行"));
            
            LoadData();
        }

        public Dictionary<string, IBackPackItemInfo> BackPackItemInfos { get; } =
            new Dictionary<string, IBackPackItemInfo>();

        public Dictionary<string, int> BackPackItemList { get; } = new Dictionary<string, int>();

        public IBackPackSystem AddBackPackItemInfos(string key, IBackPackItemInfo backPackItemInfo)
        {
            BackPackItemInfos.Add(key, backPackItemInfo);
            return this;
        }

        public void AddBackPackItemList(string key, int count)
        {
            if (BackPackItemList.ContainsKey(key))
            {
                BackPackItemList[key] += count;
            }
            else
            {
                BackPackItemList.Add(key, count);
            }
        }

        public void SaveData()
        {
            string backPackItemJson = JsonUtility.ToJson(BackPackItemList);
            
            PlayerPrefs.SetString("BackPackItemList", backPackItemJson);
            PlayerPrefs.Save();
        }

        public void LoadData()
        {
            string backPackItemJson = PlayerPrefs.GetString("BackPackItemList", "");

            Dictionary<string, int> loadedBackPackItemListData =
                JsonUtility.FromJson<Dictionary<string, int>>(backPackItemJson);

            BackPackItemList.Clear();

            if (loadedBackPackItemListData != null)
            {
                foreach (var (key, value) in loadedBackPackItemListData)
                {
                    BackPackItemList.Add(key, value);
                }
            }
        }
    }
}