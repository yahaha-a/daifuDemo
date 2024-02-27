using System.Collections.Generic;
using System.Linq;
using QFramework;
using UnityEngine;

namespace daifuDemo
{
    [System.Serializable]
    public class SerializableCurrentOwnMenuItemList
    {
        public List<SerializableKeyValuePair> items;

        [System.Serializable]
        public class SerializableKeyValuePair
        {
            public string key;
            public ICurrentOwnMenuItemInfo Value;
        }
    }
    
    public interface IMenuSystem : ISystem
    {
        Dictionary<string, IMenuItemInfo> MenuItemInfos { get; }
        
        Dictionary<string, ICurrentOwnMenuItemInfo> CurrentOwnMenuItems { get; }
        
        List<ITodayMenuItemInfo> TodayMenuItems { get; }

        IMenuSystem AddMenuItemInfos(string key, IMenuItemInfo menuItemInfo);

        void AddTodayMenuItems(ITodayMenuItemInfo todayMenuItemInfo);

        void CalculateCanMakeNumber(ICurrentOwnMenuItemInfo currentOwnMenuItemInfo);

        void UpgradeMenu(string key);

        void SaveData();

        void LoadData();
    }
    
    public class MenuSystem : AbstractSystem, IMenuSystem
    {
        private IBackPackSystem _backPackSystem;
        
        protected override void OnInit()
        {
            _backPackSystem = this.GetSystem<IBackPackSystem>();
            
            this.AddMenuItemInfos(MenuItemConfig.NormalFishsushiKey, new MenuItemInfo()
                    .WithName("普通鱼寿司")
                    .WithIcon(null)
                    .WithDescription("普通鱼寿司，可以吃")
                    .WithUnlockNeed(0)
                    .WithScore(140)
                    .WithCopies(1)
                    .WithRequiredIngredientsAmount(new List<(string, int)>()
                    {
                        (BackPackItemConfig.NormalFishPiecesKey, 10)
                    })
                    .WithRankWithCost(new List<(int, float)>()
                    {
                        (1, 100), (2, 150), (3, 200)
                    })
                    .WithUpgradeNeedItems(new List<(int, string, int)>()
                    {
                        (1, BackPackItemConfig.NormalFishPiecesKey, 20),
                        (2, BackPackItemConfig.NormalFishPiecesKey, 40),
                        (3, BackPackItemConfig.NormalFishPiecesKey, 80)
                    }))
                .AddMenuItemInfos(MenuItemConfig.PteroissushiKey, new MenuItemInfo()
                    .WithName("狮子鱼寿司")
                    .WithIcon(null)
                    .WithDescription("狮子鱼寿司, 一般")
                    .WithUnlockNeed(0)
                    .WithScore(150)
                    .WithCopies(1)
                    .WithRequiredIngredientsAmount(new List<(string, int)>()
                    {
                        (BackPackItemConfig.PteroisFishPiecesKey, 10)
                    })
                    .WithRankWithCost(new List<(int, float)>()
                    {
                        (1, 130), (2, 160), (3, 180)
                    })
                    .WithUpgradeNeedItems(new List<(int, string, int)>()
                    {
                        (1, BackPackItemConfig.PteroisFishPiecesKey, 10),
                        (2, BackPackItemConfig.PteroisFishPiecesKey, 50),
                        (3, BackPackItemConfig.PteroisFishPiecesKey, 70)
                    }));

            LoadData();
            
            foreach (var (key, menuItemInfo) in MenuItemInfos)
            {
                if (!CurrentOwnMenuItems.ContainsKey(key))
                {
                    CurrentOwnMenuItems.Add(key, new CurrentOwnMenuItemInfo()
                        .WithKey(key)
                        .WithRank(1)
                        .WithUnlock(menuItemInfo.UnLockNeed == 0 ? true : false));
                }
            }
            
            foreach (var (key, currentOwnMenuItem) in CurrentOwnMenuItems)
            {
                CalculateCanMakeNumber(currentOwnMenuItem);
            }
        }

        public Dictionary<string, IMenuItemInfo> MenuItemInfos { get; } = new Dictionary<string, IMenuItemInfo>();

        public Dictionary<string, ICurrentOwnMenuItemInfo> CurrentOwnMenuItems { get; } =
            new Dictionary<string, ICurrentOwnMenuItemInfo>();

        public List<ITodayMenuItemInfo> TodayMenuItems { get; } = new List<ITodayMenuItemInfo>();

        public IMenuSystem AddMenuItemInfos(string key, IMenuItemInfo menuItemInfo)
        {
            MenuItemInfos.Add(key, menuItemInfo);
            return this;
        }

        public void AddTodayMenuItems(ITodayMenuItemInfo todayMenuItemInfo)
        {
            TodayMenuItems.Add(todayMenuItemInfo);
        }

        public void CalculateCanMakeNumber(ICurrentOwnMenuItemInfo currentOwnMenuItem)
        {
            bool ifCanMake = true;
            int canMakeNumber = 99999;
            
            foreach (var (ingredientKey, count) in MenuItemInfos[currentOwnMenuItem.Key].RequiredIngredientsAmount)
            {
                if (_backPackSystem.SuShiBackPackItemList[ingredientKey] > count)
                {
                    int currentCanMakeNumber = _backPackSystem.SuShiBackPackItemList[ingredientKey] / count;
                    if (currentCanMakeNumber < canMakeNumber)
                    {
                        canMakeNumber = currentCanMakeNumber;
                    }
                }
                else
                {
                    ifCanMake = false;
                }
            }

            currentOwnMenuItem.MeetCondition = ifCanMake;
            currentOwnMenuItem.CanMakeNumber = canMakeNumber;
        }

        public void UpgradeMenu(string key)
        {
            foreach (var (rank, cost) in MenuItemInfos[key].RankWithCost)
            {
                if (rank > CurrentOwnMenuItems[key].Rank)
                {
                    CurrentOwnMenuItems[key].Rank++;
                    
                    foreach (var (backPackKey, amount) in MenuItemInfos[key].RequiredIngredientsAmount)
                    {
                        _backPackSystem.SuShiBackPackItemList[backPackKey] -= amount;
                    }
                    return;
                }
            }
        }


        public void SaveData()
        {
            SerializableCurrentOwnMenuItemList serializableItemList = new SerializableCurrentOwnMenuItemList
            {
                items = CurrentOwnMenuItems.Select(kvp => new SerializableCurrentOwnMenuItemList.SerializableKeyValuePair
                {
                    key = kvp.Key,
                    Value = kvp.Value
                }).ToList()
            };

            string currentMenuItemJson = JsonUtility.ToJson(serializableItemList);
            PlayerPrefs.SetString("currentMenuItemJson_data", currentMenuItemJson);
        }

        public void LoadData()
        {
            string currentMenuItemJson = PlayerPrefs.GetString("currentMenuItemJson_data", "");

            SerializableCurrentOwnMenuItemList loadedItemList =
                JsonUtility.FromJson<SerializableCurrentOwnMenuItemList>(currentMenuItemJson);

            CurrentOwnMenuItems.Clear();

            if (loadedItemList != null && loadedItemList.items != null)
            {
                foreach (var kvp in loadedItemList.items)
                {
                    CurrentOwnMenuItems.Add(kvp.key, kvp.Value);
                }
            }
        }
    }
}