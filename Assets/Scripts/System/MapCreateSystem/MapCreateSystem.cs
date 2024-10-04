using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Global;
using QFramework;
using UnityEngine;

namespace daifuDemo
{
    public interface IMapCreateSystem : ISystem
    {
        void LoadItemsFromXmlFile(string name);
    }

    public class MapCreateSystem : AbstractSystem, IMapCreateSystem
    {
        public Transform FishRoot;
        public Transform TreasureChestRoot;
        public Transform DestructibleRoot;
        
        private static ResLoader _resLoader = ResLoader.Allocate();

        private List<IMapCreateItemInfo> _createItemInfos = new List<IMapCreateItemInfo>();

        protected override void OnInit()
        {
            
        }

        public void LoadItemsFromXmlFile(string name)
        {
            string directoryPath = Path.Combine(Application.dataPath, "../Assets/Art/Archive");
            string filePath = Path.Combine(directoryPath, $"{name}.xml");

            if (File.Exists(filePath))
            {
                string xmlData = File.ReadAllText(filePath);
                LoadItemsFromXml(xmlData);
            }
            else
            {
                Debug.LogError($"File not found at path: {filePath}");
            }
        }

        private void LoadItemsFromXml(string xmlData)
        {
            var xml = XDocument.Parse(xmlData);

            foreach (var item in xml.Descendants("FishItems").Descendants("Item"))
            {
                var mapCreateItemInfo = CreateMapItemFromXmlElement(item);
                mapCreateItemInfo.WithType(CreateItemType.Fish);
                
                //TODO 实现两种类型鱼的细化
                if (mapCreateItemInfo.Key == "NormalFish")
                {
                    GameObject normalFishPrefab = _resLoader.LoadSync<GameObject>("NormalFish");
                    normalFishPrefab.GetComponent<NormalFish>().StartPosition = mapCreateItemInfo.Position;
                    normalFishPrefab.GetComponent<NormalFish>().RangeOfMovement = mapCreateItemInfo.Range;
                    mapCreateItemInfo.WithPrefab(normalFishPrefab);
                }
                else if (mapCreateItemInfo.Key == "AggressiveFish")
                {
                    GameObject aggressiveFishPrefab = _resLoader.LoadSync<GameObject>("AggressiveFish");
                    aggressiveFishPrefab.GetComponent<Pterois>().StartPosition = mapCreateItemInfo.Position;
                    aggressiveFishPrefab.GetComponent<Pterois>().RangeOfMovement = mapCreateItemInfo.Range;
                    mapCreateItemInfo.WithPrefab(aggressiveFishPrefab);
                }
                
                _createItemInfos.Add(mapCreateItemInfo);
            }

            foreach (var item in xml.Descendants("TreasureChestsItems").Descendants("Item"))
            {
                var mapCreateItemInfo = CreateMapItemFromXmlElement(item);
                mapCreateItemInfo.WithType(CreateItemType.TreasureChest);
                
                GameObject treasureChestPrefab = _resLoader.LoadSync<GameObject>("TreasureChest");
                treasureChestPrefab.GetComponent<TreasureBox>().key = mapCreateItemInfo.Key;
                
                mapCreateItemInfo.WithPrefab(treasureChestPrefab);
                _createItemInfos.Add(mapCreateItemInfo);
            }

            foreach (var item in xml.Descendants("DestructibleItems").Descendants("Item"))
            {
                var mapCreateItemInfo = CreateMapItemFromXmlElement(item);
                mapCreateItemInfo.WithType(CreateItemType.Destructible);
                
                GameObject strikeItemPrefab = _resLoader.LoadSync<GameObject>("StrikeItem");
                strikeItemPrefab.GetComponent<StrikeItem>().key = mapCreateItemInfo.Key;
                
                mapCreateItemInfo.WithPrefab(strikeItemPrefab);
                _createItemInfos.Add(mapCreateItemInfo);
            }
        }

        private IMapCreateItemInfo CreateMapItemFromXmlElement(XElement itemElement)
        {
            var key = itemElement.Attribute("key").Value;
            var x = float.Parse(itemElement.Attribute("x").Value);
            var y = float.Parse(itemElement.Attribute("y").Value);
            var range = float.Parse(itemElement.Attribute("range").Value);
            var number = int.Parse(itemElement.Attribute("number").Value);

            return new MapCreateItemInfo()
                .WithKey(key)
                .WithPosition(new Vector3(x, y, 0))
                .WithRange(range)
                .WithNumber(number);
        }

        public void CreateMap()
        {
            foreach (var item in _createItemInfos)
            {
                if (item.Type == CreateItemType.Fish)
                {
                    item.Prefab.InstantiateWithParent(FishRoot);
                }
                else if (item.Type == CreateItemType.TreasureChest)
                {
                    item.Prefab.InstantiateWithParent(TreasureChestRoot);
                }
                else if (item.Type == CreateItemType.Destructible)
                {
                    item.Prefab.InstantiateWithParent(DestructibleRoot);
                }
            }
        }
    }
}
