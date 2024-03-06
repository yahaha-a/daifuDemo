using System.Collections;
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
        
        LinkedList<ITodayMenuItemInfo> TodayMenuItems { get; }

        Queue<IPreparationDishInfo> PreparationDishes { get; }
        
        Queue<IPreparationDishInfo> MakingDishes { get; }
        
        Queue<IPreparationDishInfo> FinishedDishes { get; }

        void CalculateCanMakeNumber(ICurrentOwnMenuItemInfo currentOwnMenuItem);
        
        bool IfCanUpgradeMenu(string key);
        
        void UpgradeMenu(string key);

        bool IfCanChangeTodayMenuItemAmount(string key, int changeNumber);

        void ChangeTodayMenuItemAmount(string key, int changeNumber);
        
        void WhenTodayMenuItemAmountWithOneThenAutoSupply();

        void RemoveTodayMenuItem();

        void AddPreparationDishes(string menuKey);

        IEnumerator CreatePreparationDishes(float cookSpeed);

        string TakeAFinishedDish();

        public string GetARandomDish();

        void SaveData();

        void LoadData();
    }
    
    public class MenuSystem : AbstractSystem, IMenuSystem
    {
        private IBackPackSystem _backPackSystem;

        private IUIGamesushiPanelModel _uiGamesushiPanelModel;
        
        protected override void OnInit()
        {
            _backPackSystem = this.GetSystem<IBackPackSystem>();

            _uiGamesushiPanelModel = this.GetModel<IUIGamesushiPanelModel>();

            this.AddMenuItemInfos(MenuItemConfig.NormalFishsushiKey, new MenuItemInfo()
                    .WithName("普通鱼寿司")
                    .WithIcon(null)
                    .WithDescription("普通鱼寿司，可以吃")
                    .WithUnlockNeed(0)
                    .WithScore(140)
                    .WithCopies(1)
                    .WithMaxRank(4)
                    .WithMakeNeedTime(4)
                    .WithRequiredIngredientsAmount(new List<(string, int)>()
                    {
                        (BackPackItemConfig.NormalFishPiecesKey, 10)
                    })
                    .WithRankWithCost(new List<(int, float)>()
                    {
                        (1, 100), (2, 150), (3, 200), (4, 220)
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
                    .WithMaxRank(4)
                    .WithMakeNeedTime(5)
                    .WithRequiredIngredientsAmount(new List<(string, int)>()
                    {
                        (BackPackItemConfig.PteroisFishPiecesKey, 10)
                    })
                    .WithRankWithCost(new List<(int, float)>()
                    {
                        (1, 130), (2, 160), (3, 180), (4, 200)
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
                        .WithUnlock(menuItemInfo.UnLockNeed == 0));
                }
            }
            
            foreach (var (key, currentOwnMenuItem) in CurrentOwnMenuItems)
            {
                CalculateCanMakeNumber(currentOwnMenuItem);
            }

            _uiGamesushiPanelModel.MaxTodayMenuAmount.RegisterWithInitValue(value =>
            {
                for (int i = 1; i <= value; i++)
                {
                    LinkedListNode<ITodayMenuItemInfo> todayMenuItemNode = new LinkedListNode<ITodayMenuItemInfo>(
                        new TodayMenuItemInfo()
                            .WithNode(i)
                            .WithKey(null)
                            .WithAmount(0));
                    TodayMenuItems.AddLast(todayMenuItemNode);
                }
            });
        }

        public Dictionary<string, IMenuItemInfo> MenuItemInfos { get; } = new Dictionary<string, IMenuItemInfo>();

        public Dictionary<string, ICurrentOwnMenuItemInfo> CurrentOwnMenuItems { get; } =
            new Dictionary<string, ICurrentOwnMenuItemInfo>();

        public LinkedList<ITodayMenuItemInfo> TodayMenuItems { get; } = new LinkedList<ITodayMenuItemInfo>();

        public Queue<IPreparationDishInfo> PreparationDishes { get; } = new Queue<IPreparationDishInfo>();

        public Queue<IPreparationDishInfo> MakingDishes { get; } =
            new Queue<IPreparationDishInfo>();

        public Queue<IPreparationDishInfo> FinishedDishes { get; } =
            new Queue<IPreparationDishInfo>();

        private MenuSystem AddMenuItemInfos(string key, IMenuItemInfo menuItemInfo)
        {
            MenuItemInfos.Add(key, menuItemInfo);
            return this;
        }

        public void CalculateCanMakeNumber(ICurrentOwnMenuItemInfo currentOwnMenuItem)
        {
            bool ifCanMake = true;
            int canMakeNumber = 99999;
            
            foreach (var (ingredientKey, count) in MenuItemInfos[currentOwnMenuItem.Key.Value].RequiredIngredientsAmount)
            {
                if (_backPackSystem.SuShiBackPackItemList[ingredientKey] >= count)
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
                    canMakeNumber = 0;
                }
            }

            currentOwnMenuItem.MeetCondition.Value = ifCanMake;
            currentOwnMenuItem.CanMakeNumber.Value = canMakeNumber;
        }

        public bool IfCanUpgradeMenu(string key)
        {
            if (CurrentOwnMenuItems[key].Rank.Value == MenuItemInfos[key].MaxRank)
            {
                return false;
            }
            
            foreach (var (currentRank, backPackKey, amount) in MenuItemInfos[key].UpgradeNeedItems)
            {
                if (CurrentOwnMenuItems[key].Rank.Value == currentRank)
                {
                    if (_backPackSystem.SuShiBackPackItemList[backPackKey] < amount)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public void UpgradeMenu(string key)
        {
            foreach (var (currentRank, backPackKey, amount) in MenuItemInfos[key].UpgradeNeedItems)
            {
                if (CurrentOwnMenuItems[key].Rank.Value == currentRank)
                {
                    _backPackSystem.SuShiBackPackItemList[backPackKey] -= amount;
                }
            }
            CalculateCanMakeNumber(CurrentOwnMenuItems[key]);
                        
            CurrentOwnMenuItems[key].Rank.Value++;
        }

        public bool IfCanChangeTodayMenuItemAmount(string key, int changeNumber)
        {
            if (changeNumber > 0)
            {
                foreach (var (backPackKey, needAmount) in MenuItemInfos[key].RequiredIngredientsAmount)
                {
                    if (_backPackSystem.SuShiBackPackItemList[backPackKey] < needAmount * changeNumber)
                    {
                        return false;
                    }
                }
            }

            if (TodayMenuItems.FirstOrDefault(item => item.Key.Value == key)?.Amount.Value + changeNumber < 0)
            {
                return false;
            }
            return true;
        }

        public void ChangeTodayMenuItemAmount(string key, int changeNumber)
        {
            foreach (var (backPackKey, needAmount) in MenuItemInfos[key].RequiredIngredientsAmount)
            {
                _backPackSystem.SuShiBackPackItemList[backPackKey] -= needAmount * changeNumber;
            }

            var todayMenuItem = TodayMenuItems.FirstOrDefault(item =>
                item.Node == _uiGamesushiPanelModel.CurrentSelectTodayMenuItemNode.Value);

            if (todayMenuItem != null)
            {
                todayMenuItem.Key.Value = key;
                todayMenuItem.Amount.Value += changeNumber;
            }
            
            CalculateCanMakeNumber(CurrentOwnMenuItems[key]);
        }
        
        public void WhenTodayMenuItemAmountWithOneThenAutoSupply()
        {
            var item = TodayMenuItems.FirstOrDefault(item =>
                item.Node == _uiGamesushiPanelModel.CurrentSelectTodayMenuItemNode.Value);
            
            if (item != null)
            {
                ActionKit.OnUpdate.Register(() =>
                {
                    if (item.Amount.Value == 0)
                    {
                        item.Amount.Value++;
                    }
                });
            }
        }

        public void RemoveTodayMenuItem()
        {
            var item = TodayMenuItems.FirstOrDefault(item =>
                item.Node == _uiGamesushiPanelModel.CurrentSelectTodayMenuItemNode.Value);

            if (item != null)
            {
                ChangeTodayMenuItemAmount(item.Key.Value, -item.Amount.Value);
                item.Key.Value = null;
            }
        }

        public void AddPreparationDishes(string menuKey)
        {
            if (menuKey != null)
            {
                IPreparationDishInfo preparationDish = new PreparationDishInfo()
                    .WithKey(menuKey)
                    .WithMakeNeedTime(MenuItemInfos[menuKey].MakeNeedTime);
            
                PreparationDishes.Enqueue(preparationDish);
            }
        }

        public IEnumerator CreatePreparationDishes(float cookSpeed)
        {
            if (FinishedDishes.Count + MakingDishes.Count >= 5)
            {
                yield break;
            }
            
            if (PreparationDishes.Count > 0)
            {
                var preparationDish = PreparationDishes.Dequeue();
                MakingDishes.Enqueue(preparationDish);
                Events.CookerMakingDishesQueueAdd?.Trigger(preparationDish, cookSpeed);
                
                while (preparationDish.MakeNeedTime > 0)
                {
                    preparationDish.MakeNeedTime -= Time.deltaTime * cookSpeed;
                    yield return null;
                }
                FinishedDishes.Enqueue(MakingDishes.Dequeue());
            }
        }

        public string TakeAFinishedDish()
        {
            if (FinishedDishes.Count > 0)
            {
                return FinishedDishes.Dequeue().Key;
            }

            return null;
        }


        public string GetARandomDish()
        {
            var random = new System.Random();

            var availableMenuItems = TodayMenuItems.Where(item => item.Amount.Value > 0).ToList();

            if (availableMenuItems.Any())
            {
                var randomMenuItem = availableMenuItems[random.Next(availableMenuItems.Count)];

                randomMenuItem.Amount.Value--;

                return randomMenuItem.Key.Value;
            }

            return null;
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