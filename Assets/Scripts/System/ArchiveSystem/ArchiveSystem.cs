using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using QFramework;
using UnityEngine;
using Newtonsoft.Json;

namespace daifuDemo
{
    [System.Serializable]
    public class SerializableBackPackItemList
    {
        public string BackPackItemKey;
        public List<SerializableKeyValuePair> Items;

        [System.Serializable]
        public class SerializableKeyValuePair
        {
            public string Key;
            public int Value;
        }
    }

    [System.Serializable]
    public class SaveDataContainer
    {
        public List<SerializableBackPackItemList> BackPackData;

        public SaveDataContainer()
        {
            BackPackData = new List<SerializableBackPackItemList>();
        }
    }

    public interface IArchiveSystem : ISystem
    {
        void SaveData(Dictionary<string, int> newData, string backPackName);
        
        void LoadData(Dictionary<string, int> targetDictionary, string backPackName);

        void CreateEmptyArchive(string archiveName);

        void DeleteSaveData(string archiveName);
    }

    public class ArchiveSystem : AbstractSystem, IArchiveSystem
    {
        private IGameGlobalModel _gameGlobalModel;
        private string directoryPath;
        private string saveFilePath;

        protected override void OnInit()
        {
            _gameGlobalModel = this.GetModel<IGameGlobalModel>();
            directoryPath = Path.Combine(Application.dataPath, "../Assets/Art/GameArchive");

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }

        public void SaveData(Dictionary<string, int> newData, string backPackName)
        {
            SaveDataContainer saveData = LoadExistingData() ?? new SaveDataContainer();

            var targetItemList = saveData.BackPackData.FirstOrDefault(b => b.BackPackItemKey == backPackName);
            if (targetItemList == null)
            {
                targetItemList = new SerializableBackPackItemList { BackPackItemKey = backPackName, Items = new List<SerializableBackPackItemList.SerializableKeyValuePair>() };
                saveData.BackPackData.Add(targetItemList);
            }

            foreach (var kvp in newData)
            {
                var existingItem = targetItemList.Items.FirstOrDefault(item => item.Key == kvp.Key);
                if (existingItem != null)
                {
                    existingItem.Value = kvp.Value;
                }
                else
                {
                    targetItemList.Items.Add(new SerializableBackPackItemList.SerializableKeyValuePair
                    {
                        Key = kvp.Key,
                        Value = kvp.Value
                    });
                }
            }

            string jsonData = JsonConvert.SerializeObject(saveData, Formatting.Indented);
            File.WriteAllText(saveFilePath, jsonData);
        }

        private SaveDataContainer LoadExistingData()
        {
            saveFilePath = Path.Combine(directoryPath, $"{_gameGlobalModel.CurrentArchiveName.Value}.json");
            
            if (File.Exists(saveFilePath))
            {
                string jsonData = File.ReadAllText(saveFilePath);
                return JsonConvert.DeserializeObject<SaveDataContainer>(jsonData);
            }
            return null;
        }

        public void LoadData(Dictionary<string, int> targetDictionary, string backPackName)
        {
            SaveDataContainer loadedData = LoadExistingData();
            if (loadedData == null)
            {
                return;
            }

            var sourceItemList = loadedData.BackPackData.FirstOrDefault(b => b.BackPackItemKey == backPackName);
            if (sourceItemList == null)
            {
                return;
            }

            targetDictionary.Clear();
            if (sourceItemList.Items != null)
            {
                foreach (var kvp in sourceItemList.Items)
                {
                    targetDictionary[kvp.Key] = kvp.Value;
                }
            }
        }

        public void CreateEmptyArchive(string archiveName)
        {
            string newFilePath = Path.Combine(directoryPath, $"{archiveName}.json");

            if (File.Exists(newFilePath))
            {
                return;
            }

            SaveDataContainer emptySaveData = new SaveDataContainer();

            string emptyJsonData = JsonConvert.SerializeObject(emptySaveData, Formatting.Indented);
            File.WriteAllText(newFilePath, emptyJsonData);
        }

        
        public void DeleteSaveData(string archiveName)
        {
            string filepath = Path.Combine(directoryPath, $"{archiveName}.json");
            
            if (File.Exists(filepath))
            {
                File.Delete(filepath);
            }
            else
            {
                
            }
        }
    }
}
