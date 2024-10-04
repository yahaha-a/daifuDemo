using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Global;
using QFramework;
using UnityEditor;
using UnityEngine;

namespace MapEditor
{
    public interface IMapEditorSystem : ISystem
    {
        Dictionary<CreateItemName, IMapEditorInfo> _mapEditorInfos { get; }
        
        Dictionary<CreateItemName, List<ICreateItemInfo>> BarrierItems { get; }
        
        Dictionary<CreateItemName, List<ICreateItemInfo>> RoleItems { get; }
        
        Dictionary<CreateItemName, List<ICreateItemInfo>> FishCreateItems { get; }

        Dictionary<CreateItemName, List<ICreateItemInfo>> TreasureChestsItems { get; }

        Dictionary<CreateItemName, List<ICreateItemInfo>> DestructibleItems { get; }
        
        Dictionary<CreateItemName, List<ICreateItemInfo>> DropsItems { get; }

        void CreateEmptySave(string name);

        void SaveItemsToXml(string name);

        void LoadItemsFromXml(string name);

        int GetMaxSerialNumberInXml(string name);
    }
    
    public class MapEditorSystem : AbstractSystem, IMapEditorSystem
    {
        private static ResLoader _resLoader = ResLoader.Allocate();

        private IMapEditorModel _mapEditorModel;

        //TODO
        public Dictionary<CreateItemName, IMapEditorInfo> _mapEditorInfos { get; } =
            new Dictionary<CreateItemName, IMapEditorInfo>()
            {
                {
                    CreateItemName.Null,
                    new MapEditorInfo()
                        .WithKey(CreateItemName.Null)
                        .WithOptionType(OptionType.Null)
                        .WithCreateItemType(CreateItemType.Null)
                        .WithName("空")
                },

                {
                    CreateItemName.Clod,
                    new MapEditorInfo()
                        .WithKey(CreateItemName.Clod)
                        .WithOptionType(OptionType.Single)
                        .WithCreateItemType(CreateItemType.Barrier)
                        .WithName("土块")
                },
                
                {
                    CreateItemName.Dave,
                    new MapEditorInfo()
                        .WithKey(CreateItemName.Dave)
                        .WithOptionType(OptionType.Single)
                        .WithCreateItemType(CreateItemType.Role)
                        .WithName("戴夫")
                },
                
                {
                    CreateItemName.NormalFish,
                    new MapEditorInfo()
                        .WithKey(CreateItemName.NormalFish)
                        .WithOptionType(OptionType.Range)
                        .WithCreateItemType(CreateItemType.Fish)
                        .WithName("普通鱼")
                },
                
                {
                    CreateItemName.PteroisFish,
                    new MapEditorInfo()
                        .WithKey(CreateItemName.PteroisFish)
                        .WithOptionType(OptionType.Range)
                        .WithCreateItemType(CreateItemType.Fish)
                        .WithName("狮子鱼")
                },

                {
                    CreateItemName.ToolTreasureChest,
                    new MapEditorInfo()
                        .WithKey(CreateItemName.ToolTreasureChest)
                        .WithOptionType(OptionType.Single)
                        .WithCreateItemType(CreateItemType.TreasureChests)
                        .WithName("材料宝箱")
                },
                
                {
                    CreateItemName.SpiceTreasureChest,
                    new MapEditorInfo()
                        .WithKey(CreateItemName.SpiceTreasureChest)
                        .WithOptionType(OptionType.Single)
                        .WithCreateItemType(CreateItemType.TreasureChests)
                        .WithName("调味品宝箱")
                },
                
                {
                    CreateItemName.KelpPlants,
                    new MapEditorInfo()
                        .WithKey(CreateItemName.KelpPlants)
                        .WithOptionType(OptionType.Single)
                        .WithCreateItemType(CreateItemType.Destructible)
                        .WithName("海带植物")
                },
                
                {
                    CreateItemName.CoralPlants,
                    new MapEditorInfo()
                        .WithKey(CreateItemName.CoralPlants)
                        .WithOptionType(OptionType.Single)
                        .WithCreateItemType(CreateItemType.Destructible)
                        .WithName("珊瑚礁")
                },
                
                {
                    CreateItemName.CopperOre,
                    new MapEditorInfo()
                        .WithKey(CreateItemName.CopperOre)
                        .WithOptionType(OptionType.Single)
                        .WithCreateItemType(CreateItemType.Destructible)
                        .WithName("铜矿石")
                },
                
                {
                    CreateItemName.Cordage,
                    new MapEditorInfo()
                        .WithKey(CreateItemName.Cordage)
                        .WithOptionType(OptionType.Single)
                        .WithCreateItemType(CreateItemType.Drops)
                        .WithName("绳索")
                },
                
                {
                    CreateItemName.Wood,
                    new MapEditorInfo()
                        .WithKey(CreateItemName.Wood)
                        .WithOptionType(OptionType.Single)
                        .WithCreateItemType(CreateItemType.Drops)
                        .WithName("木头")
                },
            };

        public Dictionary<CreateItemName, List<ICreateItemInfo>> BarrierItems { get; } =
            new Dictionary<CreateItemName, List<ICreateItemInfo>>();
        
        public Dictionary<CreateItemName, List<ICreateItemInfo>> RoleItems { get; } =
            new Dictionary<CreateItemName, List<ICreateItemInfo>>();
        
        public Dictionary<CreateItemName, List<ICreateItemInfo>> FishCreateItems { get; } =
            new Dictionary<CreateItemName, List<ICreateItemInfo>>();

        public Dictionary<CreateItemName, List<ICreateItemInfo>> TreasureChestsItems { get; } =
            new Dictionary<CreateItemName, List<ICreateItemInfo>>();

        public Dictionary<CreateItemName, List<ICreateItemInfo>> DestructibleItems { get; } =
            new Dictionary<CreateItemName, List<ICreateItemInfo>>();
        
        public Dictionary<CreateItemName, List<ICreateItemInfo>> DropsItems { get; } =
            new Dictionary<CreateItemName, List<ICreateItemInfo>>();
        
        protected override void OnInit()
        {
            _mapEditorModel = this.GetModel<IMapEditorModel>();
            
            MapEditorEvents.CreateMapEditorItem.Register(() =>
            {
                ICreateItemInfo currentCreateItemInfo = new CreateItemInfo()
                    .WithKey(_mapEditorModel.CurrentMapEditorName.Value)
                    .WithSerialNumber(_mapEditorModel.CurrentSerialNumber.Value)
                    .WithX(_mapEditorModel.CurrentMousePosition.Value.x)
                    .WithY(_mapEditorModel.CurrentMousePosition.Value.y)
                    .WithRange(_mapEditorModel.CurrentSelectRange.Value)
                    .WithNumber(_mapEditorModel.CurrentCreateItemNumber.Value);
                
                if (_mapEditorInfos[_mapEditorModel.CurrentMapEditorName.Value].CreateItemType == CreateItemType.Fish)
                {
                    if (FishCreateItems.ContainsKey(_mapEditorModel.CurrentMapEditorName.Value))
                    {
                        FishCreateItems[_mapEditorModel.CurrentMapEditorName.Value].Add(currentCreateItemInfo);
                    }
                    else
                    {
                        FishCreateItems.Add(_mapEditorModel.CurrentMapEditorName.Value, new List<ICreateItemInfo>());
                        FishCreateItems[_mapEditorModel.CurrentMapEditorName.Value].Add(currentCreateItemInfo);
                    }
                }
                else if (_mapEditorInfos[_mapEditorModel.CurrentMapEditorName.Value].CreateItemType == CreateItemType.TreasureChests)
                {
                    if (TreasureChestsItems.ContainsKey(_mapEditorModel.CurrentMapEditorName.Value))
                    {
                        TreasureChestsItems[_mapEditorModel.CurrentMapEditorName.Value].Add(currentCreateItemInfo);
                    }
                    else
                    {
                        TreasureChestsItems.Add(_mapEditorModel.CurrentMapEditorName.Value, new List<ICreateItemInfo>());
                        TreasureChestsItems[_mapEditorModel.CurrentMapEditorName.Value].Add(currentCreateItemInfo);
                    }
                }
                else if (_mapEditorInfos[_mapEditorModel.CurrentMapEditorName.Value].CreateItemType == CreateItemType.Destructible)
                {
                    if (DestructibleItems.ContainsKey(_mapEditorModel.CurrentMapEditorName.Value))
                    {
                        DestructibleItems[_mapEditorModel.CurrentMapEditorName.Value].Add(currentCreateItemInfo);
                    }
                    else
                    {
                        DestructibleItems.Add(_mapEditorModel.CurrentMapEditorName.Value, new List<ICreateItemInfo>());
                        DestructibleItems[_mapEditorModel.CurrentMapEditorName.Value].Add(currentCreateItemInfo);
                    }
                }
                else if (_mapEditorInfos[_mapEditorModel.CurrentMapEditorName.Value].CreateItemType == CreateItemType.Barrier)
                {
                    if (BarrierItems.ContainsKey(_mapEditorModel.CurrentMapEditorName.Value))
                    {
                        BarrierItems[_mapEditorModel.CurrentMapEditorName.Value].Add(currentCreateItemInfo);
                    }
                    else
                    {
                        BarrierItems.Add(_mapEditorModel.CurrentMapEditorName.Value, new List<ICreateItemInfo>());
                        BarrierItems[_mapEditorModel.CurrentMapEditorName.Value].Add(currentCreateItemInfo);
                    }
                }
                else if (_mapEditorInfos[_mapEditorModel.CurrentMapEditorName.Value].CreateItemType == CreateItemType.Role)
                {
                    if (RoleItems.ContainsKey(_mapEditorModel.CurrentMapEditorName.Value))
                    {
                        RoleItems[_mapEditorModel.CurrentMapEditorName.Value].Add(currentCreateItemInfo);
                    }
                    else
                    {
                        RoleItems.Add(_mapEditorModel.CurrentMapEditorName.Value, new List<ICreateItemInfo>());
                        RoleItems[_mapEditorModel.CurrentMapEditorName.Value].Add(currentCreateItemInfo);
                    }
                }
                else if (_mapEditorInfos[_mapEditorModel.CurrentMapEditorName.Value].CreateItemType == CreateItemType.Drops)
                {
                    if (DropsItems.ContainsKey(_mapEditorModel.CurrentMapEditorName.Value))
                    {
                        DropsItems[_mapEditorModel.CurrentMapEditorName.Value].Add(currentCreateItemInfo);
                    }
                    else
                    {
                        DropsItems.Add(_mapEditorModel.CurrentMapEditorName.Value, new List<ICreateItemInfo>());
                        DropsItems[_mapEditorModel.CurrentMapEditorName.Value].Add(currentCreateItemInfo);
                    }
                }

                _mapEditorModel.CurrentSerialNumber.Value++;
                
                MapEditorEvents.ShowCreateItem?.Trigger(currentCreateItemInfo);
            });
            
            MapEditorEvents.DeleteCreateItem.Register(serialNumber =>
            {
                foreach (var (mapEditorName, createItemInfos) in FishCreateItems)
                {
                    if (createItemInfos.Find(item => item.SerialNumber == serialNumber) != null)
                    {
                        var itemToRemove = createItemInfos.Find(item => item.SerialNumber == serialNumber);
                        FishCreateItems[mapEditorName].Remove(itemToRemove);
                        MapEditorEvents.refreshCreatePanel?.Trigger();
                        return;
                    }
                }
                
                foreach (var (mapEditorName, createItemInfos) in TreasureChestsItems)
                {
                    if (createItemInfos.Find(item => item.SerialNumber == serialNumber) != null)
                    {
                        var itemToRemove = createItemInfos.Find(item => item.SerialNumber == serialNumber);
                        TreasureChestsItems[mapEditorName].Remove(itemToRemove);
                        MapEditorEvents.refreshCreatePanel?.Trigger();
                        return;
                    }
                }
                
                foreach (var (mapEditorName, createItemInfos) in DestructibleItems)
                {
                    if (createItemInfos.Find(item => item.SerialNumber == serialNumber) != null)
                    {
                        var itemToRemove = createItemInfos.Find(item => item.SerialNumber == serialNumber);
                        DestructibleItems[mapEditorName].Remove(itemToRemove);
                        MapEditorEvents.refreshCreatePanel?.Trigger();
                        return;
                    }
                }
                
                foreach (var (mapEditorName, createItemInfos) in BarrierItems)
                {
                    if (createItemInfos.Find(item => item.SerialNumber == serialNumber) != null)
                    {
                        var itemToRemove = createItemInfos.Find(item => item.SerialNumber == serialNumber);
                        BarrierItems[mapEditorName].Remove(itemToRemove);
                        MapEditorEvents.refreshCreatePanel?.Trigger();
                        return;
                    }
                }
                
                foreach (var (mapEditorName, createItemInfos) in RoleItems)
                {
                    if (createItemInfos.Find(item => item.SerialNumber == serialNumber) != null)
                    {
                        var itemToRemove = createItemInfos.Find(item => item.SerialNumber == serialNumber);
                        RoleItems[mapEditorName].Remove(itemToRemove);
                        MapEditorEvents.refreshCreatePanel?.Trigger();
                        return;
                    }
                }
                
                foreach (var (mapEditorName, createItemInfos) in DropsItems)
                {
                    if (createItemInfos.Find(item => item.SerialNumber == serialNumber) != null)
                    {
                        var itemToRemove = createItemInfos.Find(item => item.SerialNumber == serialNumber);
                        DropsItems[mapEditorName].Remove(itemToRemove);
                        MapEditorEvents.refreshCreatePanel?.Trigger();
                        return;
                    }
                }
            });
        }
        
        public void CreateEmptySave(string name)
        {
            FishCreateItems.Clear();
            TreasureChestsItems.Clear();
            DestructibleItems.Clear();
            BarrierItems.Clear();
            RoleItems.Clear();
            DropsItems.Clear();

            XmlDocument document = new XmlDocument();
            var root = document.CreateElement("Items");
            document.AppendChild(root);

            string directoryPath = Path.Combine(Application.dataPath, "../Assets/Art/Archive");

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string filePath = Path.Combine(directoryPath, $"{name}.xml");

            document.Save(filePath);
            AssetDatabase.Refresh();
            LoadItemsFromXml(name);
        }
        
        public void SaveItemsToXml(string name)
        {
            XmlDocument document = new XmlDocument();
            var root = document.CreateElement("Items");
            document.AppendChild(root);

            var fishItemsNode = document.CreateElement("FishItems");
            foreach (var itemList in FishCreateItems.Values)
            {
                foreach (var item in itemList)
                {
                    var itemNode = document.CreateElement("Item");
                    itemNode.SetAttribute("key", item.Key.ToString());
                    itemNode.SetAttribute("serialNumber", item.SerialNumber.ToString());
                    itemNode.SetAttribute("x", item.X.ToString());
                    itemNode.SetAttribute("y", item.Y.ToString());
                    itemNode.SetAttribute("range", item.Range.ToString());
                    itemNode.SetAttribute("number", item.Number.ToString());
                    fishItemsNode.AppendChild(itemNode);
                }
            }
            root.AppendChild(fishItemsNode);

            var treasureChestsNode = document.CreateElement("TreasureChestsItems");
            foreach (var itemList in TreasureChestsItems.Values)
            {
                foreach (var item in itemList)
                {
                    var itemNode = document.CreateElement("Item");
                    itemNode.SetAttribute("key", item.Key.ToString());
                    itemNode.SetAttribute("serialNumber", item.SerialNumber.ToString());
                    itemNode.SetAttribute("x", item.X.ToString());
                    itemNode.SetAttribute("y", item.Y.ToString());
                    itemNode.SetAttribute("range", item.Range.ToString());
                    itemNode.SetAttribute("number", item.Number.ToString());
                    treasureChestsNode.AppendChild(itemNode);
                }
            }
            root.AppendChild(treasureChestsNode);

            var destructibleItemsNode = document.CreateElement("DestructibleItems");
            foreach (var itemList in DestructibleItems.Values)
            {
                foreach (var item in itemList)
                {
                    var itemNode = document.CreateElement("Item");
                    itemNode.SetAttribute("key", item.Key.ToString());
                    itemNode.SetAttribute("serialNumber", item.SerialNumber.ToString());
                    itemNode.SetAttribute("x", item.X.ToString());
                    itemNode.SetAttribute("y", item.Y.ToString());
                    itemNode.SetAttribute("range", item.Range.ToString());
                    itemNode.SetAttribute("number", item.Number.ToString());
                    destructibleItemsNode.AppendChild(itemNode);
                }
            }
            root.AppendChild(destructibleItemsNode);

            var barrierItemsNode = document.CreateElement("BarrierItems");
            foreach (var itemList in BarrierItems.Values)
            {
                foreach (var item in itemList)
                {
                    var itemNode = document.CreateElement("Item");
                    itemNode.SetAttribute("key", item.Key.ToString());
                    itemNode.SetAttribute("serialNumber", item.SerialNumber.ToString());
                    itemNode.SetAttribute("x", item.X.ToString());
                    itemNode.SetAttribute("y", item.Y.ToString());
                    itemNode.SetAttribute("range", item.Range.ToString());
                    itemNode.SetAttribute("number", item.Number.ToString());
                    barrierItemsNode.AppendChild(itemNode);
                }
            }
            root.AppendChild(barrierItemsNode);

            var roleItemsNode = document.CreateElement("RoleItems");
            foreach (var itemList in RoleItems.Values)
            {
                foreach (var item in itemList)
                {
                    var itemNode = document.CreateElement("Item");
                    itemNode.SetAttribute("key", item.Key.ToString());
                    itemNode.SetAttribute("serialNumber", item.SerialNumber.ToString());
                    itemNode.SetAttribute("x", item.X.ToString());
                    itemNode.SetAttribute("y", item.Y.ToString());
                    itemNode.SetAttribute("range", item.Range.ToString());
                    itemNode.SetAttribute("number", item.Number.ToString());
                    roleItemsNode.AppendChild(itemNode);
                }
            }
            root.AppendChild(roleItemsNode);

            var dropsItemsNode = document.CreateElement("DropsItems");
            foreach (var itemList in DropsItems.Values)
            {
                foreach (var item in itemList)
                {
                    var itemNode = document.CreateElement("Item");
                    itemNode.SetAttribute("key", item.Key.ToString());
                    itemNode.SetAttribute("serialNumber", item.SerialNumber.ToString());
                    itemNode.SetAttribute("x", item.X.ToString());
                    itemNode.SetAttribute("y", item.Y.ToString());
                    itemNode.SetAttribute("range", item.Range.ToString());
                    itemNode.SetAttribute("number", item.Number.ToString());
                    dropsItemsNode.AppendChild(itemNode);
                }
            }
            root.AppendChild(dropsItemsNode);

            string directoryPath = Path.Combine(Application.dataPath, "../Assets/Art/Archive");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string filePath = Path.Combine(directoryPath, $"{name}.xml");

            document.Save(filePath);
            AssetDatabase.Refresh();
        }

        
        public void LoadItemsFromXml(string name)
        {
            FishCreateItems.Clear();
            TreasureChestsItems.Clear();
            DestructibleItems.Clear();
            BarrierItems.Clear();
            RoleItems.Clear();
            DropsItems.Clear();

            string directoryPath = Path.Combine(Application.dataPath, "../Assets/Art/Archive");
            string filePath = Path.Combine(directoryPath, $"{name}.xml");

            if (!File.Exists(filePath))
            {
                return;
            }

            XmlDocument document = new XmlDocument();
            document.Load(filePath);

            XmlNodeList fishItemsNodes = document.SelectNodes("/Items/FishItems/Item");
            LoadItemsIntoDictionary(fishItemsNodes, FishCreateItems);

            XmlNodeList treasureChestsNodes = document.SelectNodes("/Items/TreasureChestsItems/Item");
            LoadItemsIntoDictionary(treasureChestsNodes, TreasureChestsItems);

            XmlNodeList destructibleItemsNodes = document.SelectNodes("/Items/DestructibleItems/Item");
            LoadItemsIntoDictionary(destructibleItemsNodes, DestructibleItems);

            XmlNodeList barrierItemsNodes = document.SelectNodes("/Items/BarrierItems/Item");
            LoadItemsIntoDictionary(barrierItemsNodes, BarrierItems);

            XmlNodeList roleItemsNodes = document.SelectNodes("/Items/RoleItems/Item");
            LoadItemsIntoDictionary(roleItemsNodes, RoleItems);

            XmlNodeList dropsItemsNodes = document.SelectNodes("/Items/DropsItems/Item");
            LoadItemsIntoDictionary(dropsItemsNodes, DropsItems);

            MapEditorEvents.refreshCreatePanel?.Trigger();
            MapEditorEvents.LoadArchive?.Trigger();
        }


        private void LoadItemsIntoDictionary(XmlNodeList itemNodes, Dictionary<CreateItemName, List<ICreateItemInfo>> targetDictionary)
        {
            foreach (XmlNode itemNode in itemNodes)
            {
                string keyString = itemNode.Attributes["key"].Value;
                CreateItemName key = (CreateItemName)System.Enum.Parse(typeof(CreateItemName), keyString);
                int serialNumber = int.Parse(itemNode.Attributes["serialNumber"].Value);
                float x = float.Parse(itemNode.Attributes["x"].Value);
                float y = float.Parse(itemNode.Attributes["y"].Value);
                float range = float.Parse(itemNode.Attributes["range"].Value);
                int number = int.Parse(itemNode.Attributes["number"].Value);

                ICreateItemInfo newItem = new CreateItemInfo()
                    .WithKey(key)
                    .WithSerialNumber(serialNumber)
                    .WithX(x)
                    .WithY(y)
                    .WithRange(range)
                    .WithNumber(number);

                if (!targetDictionary.ContainsKey(key))
                {
                    targetDictionary[key] = new List<ICreateItemInfo>();
                }
                targetDictionary[key].Add(newItem);
            }
        }
        
        public int GetMaxSerialNumberInXml(string name)
        {
            string directoryPath = Path.Combine(Application.dataPath, "../Assets/Art/Archive");
            string filePath = Path.Combine(directoryPath, $"{name}.xml");

            XmlDocument document = new XmlDocument();
            document.Load(filePath);

            int maxSerialNumber = 0;

            XmlNodeList fishItemsNodes = document.SelectNodes("/Items/FishItems/Item");
            foreach (XmlNode itemNode in fishItemsNodes)
            {
                int serialNumber = int.Parse(itemNode.Attributes["serialNumber"].Value);
                maxSerialNumber = Mathf.Max(maxSerialNumber, serialNumber);
            }

            XmlNodeList treasureItemsNodes = document.SelectNodes("/Items/TreasureChestsItems/Item");
            foreach (XmlNode itemNode in treasureItemsNodes)
            {
                int serialNumber = int.Parse(itemNode.Attributes["serialNumber"].Value);
                maxSerialNumber = Mathf.Max(maxSerialNumber, serialNumber);
            }

            XmlNodeList destructibleItemsNodes = document.SelectNodes("/Items/DestructibleItems/Item");
            foreach (XmlNode itemNode in destructibleItemsNodes)
            {
                int serialNumber = int.Parse(itemNode.Attributes["serialNumber"].Value);
                maxSerialNumber = Mathf.Max(maxSerialNumber, serialNumber);
            }

            XmlNodeList barrierItemsNodes = document.SelectNodes("/Items/BarrierItems/Item");
            foreach (XmlNode itemNode in barrierItemsNodes)
            {
                int serialNumber = int.Parse(itemNode.Attributes["serialNumber"].Value);
                maxSerialNumber = Mathf.Max(maxSerialNumber, serialNumber);
            }

            XmlNodeList roleItemsNodes = document.SelectNodes("/Items/RoleItems/Item");
            foreach (XmlNode itemNode in roleItemsNodes)
            {
                int serialNumber = int.Parse(itemNode.Attributes["serialNumber"].Value);
                maxSerialNumber = Mathf.Max(maxSerialNumber, serialNumber);
            }

            XmlNodeList dropsItemsNodes = document.SelectNodes("/Items/DropsItems/Item");
            foreach (XmlNode itemNode in dropsItemsNodes)
            {
                int serialNumber = int.Parse(itemNode.Attributes["serialNumber"].Value);
                maxSerialNumber = Mathf.Max(maxSerialNumber, serialNumber);
            }

            return maxSerialNumber;
        }
    }
}