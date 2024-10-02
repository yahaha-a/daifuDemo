using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using QFramework;
using UnityEditor;
using UnityEngine;

namespace MapEditor
{
    public interface IMapEditorSystem : ISystem
    {
        Dictionary<MapEditorName, IMapEditorInfo> _mapEditorInfos { get; }
        
        Dictionary<MapEditorName, List<ICreateItemInfo>> FishCreateItems { get; }
        
        Dictionary<MapEditorName, List<ICreateItemInfo>> TreasureChestsItems { get; }
        
        Dictionary<MapEditorName, List<ICreateItemInfo>> DestructibleItems { get; }

        void CreateEmptySave(string name);

        void SaveItemsToXml(string name);

        void LoadItemsFromXml(string name);

        int GetMaxSerialNumberInXml(string name);
    }
    
    public class MapEditorSystem : AbstractSystem, IMapEditorSystem
    {
        private static ResLoader _resLoader = ResLoader.Allocate();

        private IMapEditorModel _mapEditorModel;

        public Dictionary<MapEditorName, IMapEditorInfo> _mapEditorInfos { get; } =
            new Dictionary<MapEditorName, IMapEditorInfo>()
            {
                {
                    MapEditorName.Null,
                    new MapEditorInfo()
                        .WithKey(MapEditorName.Null)
                        .WithOptionType(OptionType.Null)
                        .WithCreateItemType(CreateItemType.Null)
                        .WithName("空")
                },

                {
                    MapEditorName.Kelp,
                    new MapEditorInfo()
                        .WithKey(MapEditorName.Kelp)
                        .WithOptionType(OptionType.Single)
                        .WithCreateItemType(CreateItemType.Destructible)
                        .WithName("海带")
                },

                {
                    MapEditorName.NormalTreasureChest,
                    new MapEditorInfo()
                        .WithKey(MapEditorName.NormalTreasureChest)
                        .WithOptionType(OptionType.Single)
                        .WithCreateItemType(CreateItemType.TreasureChests)
                        .WithName("普通宝箱")
                },

                {
                    MapEditorName.NormalFishEditor,
                    new MapEditorInfo()
                        .WithKey(MapEditorName.NormalFishEditor)
                        .WithOptionType(OptionType.Range)
                        .WithCreateItemType(CreateItemType.Fish)
                        .WithName("普通鱼")
                }
            };

        public Dictionary<MapEditorName, List<ICreateItemInfo>> FishCreateItems { get; } =
            new Dictionary<MapEditorName, List<ICreateItemInfo>>();

        public Dictionary<MapEditorName, List<ICreateItemInfo>> TreasureChestsItems { get; } =
            new Dictionary<MapEditorName, List<ICreateItemInfo>>();

        public Dictionary<MapEditorName, List<ICreateItemInfo>> DestructibleItems { get; } =
            new Dictionary<MapEditorName, List<ICreateItemInfo>>();
        
        protected override void OnInit()
        {
            _mapEditorModel = this.GetModel<IMapEditorModel>();
            
            MapEditorEvents.CreateMapEditorItem.Register((key, coordinate) =>
            {
                ICreateItemInfo currentCreateItemInfo = new CreateItemInfo()
                    .WithKey(key)
                    .WithSerialNumber(_mapEditorModel.CurrentSerialNumber.Value)
                    .WithX(coordinate.x)
                    .WithY(coordinate.y)
                    .WithRange(coordinate.z);
                
                if (_mapEditorInfos[key].CreateItemType == CreateItemType.Fish)
                {
                    if (FishCreateItems.ContainsKey(key))
                    {
                        FishCreateItems[key].Add(currentCreateItemInfo);
                    }
                    else
                    {
                        FishCreateItems.Add(key, new List<ICreateItemInfo>());
                        FishCreateItems[key].Add(currentCreateItemInfo);
                    }
                }
                else if (_mapEditorInfos[key].CreateItemType == CreateItemType.TreasureChests)
                {
                    if (TreasureChestsItems.ContainsKey(key))
                    {
                        TreasureChestsItems[key].Add(currentCreateItemInfo);
                    }
                    else
                    {
                        TreasureChestsItems.Add(key, new List<ICreateItemInfo>());
                        TreasureChestsItems[key].Add(currentCreateItemInfo);
                    }
                }
                else if (_mapEditorInfos[key].CreateItemType == CreateItemType.Destructible)
                {
                    if (DestructibleItems.ContainsKey(key))
                    {
                        DestructibleItems[key].Add(currentCreateItemInfo);
                    }
                    else
                    {
                        DestructibleItems.Add(key, new List<ICreateItemInfo>());
                        DestructibleItems[key].Add(currentCreateItemInfo);
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
            });
        }
        
        public void CreateEmptySave(string name)
        {
            FishCreateItems.Clear();
            TreasureChestsItems.Clear();
            DestructibleItems.Clear();

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
                    destructibleItemsNode.AppendChild(itemNode);
                }
            }
            root.AppendChild(destructibleItemsNode);

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
    
            MapEditorEvents.refreshCreatePanel?.Trigger();
        }


        private void LoadItemsIntoDictionary(XmlNodeList itemNodes, Dictionary<MapEditorName, List<ICreateItemInfo>> targetDictionary)
        {
            foreach (XmlNode itemNode in itemNodes)
            {
                string keyString = itemNode.Attributes["key"].Value;
                MapEditorName key = (MapEditorName)System.Enum.Parse(typeof(MapEditorName), keyString);
                int serialNumber = int.Parse(itemNode.Attributes["serialNumber"].Value);
                float x = float.Parse(itemNode.Attributes["x"].Value);
                float y = float.Parse(itemNode.Attributes["y"].Value);
                float range = float.Parse(itemNode.Attributes["range"].Value);

                ICreateItemInfo newItem = new CreateItemInfo()
                {
                    Key = key,
                    SerialNumber = serialNumber,
                    X = x,
                    Y = y,
                    Range = range
                };

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

            return maxSerialNumber;
        }

    }
}