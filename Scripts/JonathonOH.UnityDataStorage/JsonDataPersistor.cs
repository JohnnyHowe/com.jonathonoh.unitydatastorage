using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace JonathonOH.UnityDataStorage
{
    public class JsonDataPersistor : IDataPersistor
    {
        private const string fileName = "JonathonOH.UnityStorage.Data.json";
        public string filePath;
        private bool savedDataExists;

        // key, json data
        private Dictionary<string, string> data;

        public JsonDataPersistor()
        {
#if UNITY_EDITOR
            // When in the Unity editor, we save the data to the assets folder for easy access
            filePath = Application.dataPath + Path.AltDirectorySeparatorChar + fileName;
#else
            // Correct place to save persistent data
            filePath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + defaultFileName;
#endif
            LoadSavedData();
        }

        public bool Contains(string key)
        {
            throw new System.NotImplementedException();
        }

        public void Set<T>(string key, T value, bool saveNow = true)
        {
            data[key] = JsonConvert.SerializeObject(value);
            if (saveNow) Save();
        }

        public T TryGet<T>(string key, T fallback)
        {
            if (!data.ContainsKey(key)) return fallback;
            return JsonConvert.DeserializeObject<T>(data[key]);
        }

        public async void Save()
        {
            await Save(JsonConvert.SerializeObject(data));
        }

        /// <summary>
        /// sets data to new dictionary if no data is saved
        /// sets savedDataExists if a file with json exists
        /// </summary>
        /// <returns></returns>
        private void LoadSavedData()
        {
            savedDataExists = false;
            if (!File.Exists(filePath))
            {
                data = new Dictionary<string, string>();
                return;
            }

            StreamReader reader = new StreamReader(filePath);
            string rawContents = reader.ReadToEnd();
            reader.Close();
            Dictionary<string, string> readData = JsonConvert.DeserializeObject<Dictionary<string, string>>(rawContents);
            if (readData != null)
            {
                savedDataExists = true;
                data = readData;
            }

            data = new Dictionary<string, string>();
        }

        public bool SavedDataExists()
        {
            return savedDataExists;
        }

        internal void DeleteSaveFile()
        {
            File.Delete(filePath);
        }

        /// <summary>
        /// Removes all save data but keeps a json file with no data
        /// </summary>
        internal async void ClearSaveData()
        {
            await Save("{}");
        }

        private async Task Save(string jsonString)
        {
            StreamWriter writer = new StreamWriter(filePath);
            await writer.WriteAsync(jsonString);
            writer.Close();
        }
    }
}