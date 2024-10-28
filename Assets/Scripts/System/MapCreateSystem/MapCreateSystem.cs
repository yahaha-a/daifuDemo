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

        void Reload();

        List<IMapCreateItemInfo> GetCreateItemInfos();
    }

    public class MapCreateSystem : AbstractSystem, IMapCreateSystem
    {
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

        public void Reload()
        {
            _createItemInfos.Clear();
        }

        public List<IMapCreateItemInfo> GetCreateItemInfos()
        {
            return _createItemInfos;
        }

        private void LoadItemsFromXml(string xmlData)
        {
            var xml = XDocument.Parse(xmlData);
            
            foreach (var item in xml.Descendants("BarrierItems").Descendants("Item"))
            {
                var mapCreateItemInfo = CreateMapItemFromXmlElement(item);
                mapCreateItemInfo.WithType(CreateItemType.Barrier);
                
                _createItemInfos.Add(mapCreateItemInfo);
            }
            
            foreach (var item in xml.Descendants("RoleItems").Descendants("Item"))
            {
                var mapCreateItemInfo = CreateMapItemFromXmlElement(item);
                mapCreateItemInfo.WithType(CreateItemType.Role);
                
                _createItemInfos.Add(mapCreateItemInfo);
            }

            foreach (var item in xml.Descendants("FishItems").Descendants("Item"))
            {
                var mapCreateItemInfo = CreateMapItemFromXmlElement(item);
                mapCreateItemInfo.WithType(CreateItemType.Fish);
                
                _createItemInfos.Add(mapCreateItemInfo);
            }

            foreach (var item in xml.Descendants("TreasureChestsItems").Descendants("Item"))
            {
                var mapCreateItemInfo = CreateMapItemFromXmlElement(item);
                mapCreateItemInfo.WithType(CreateItemType.TreasureChests);
                
                _createItemInfos.Add(mapCreateItemInfo);
            }

            foreach (var item in xml.Descendants("DestructibleItems").Descendants("Item"))
            {
                var mapCreateItemInfo = CreateMapItemFromXmlElement(item);
                mapCreateItemInfo.WithType(CreateItemType.Destructible);
                
                _createItemInfos.Add(mapCreateItemInfo);
            }
            
            foreach (var item in xml.Descendants("DropsItems").Descendants("Item"))
            {
                var mapCreateItemInfo = CreateMapItemFromXmlElement(item);
                mapCreateItemInfo.WithType(CreateItemType.Drops);
                
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
    }
}
