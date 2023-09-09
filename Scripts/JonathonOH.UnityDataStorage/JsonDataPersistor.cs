using System.Collections.Generic;
using System.IO;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace JonathonOH.UnityDataStorage
{
    public class JsonDataPersistor : IDataPersistor
    {
        private const string fileName = "JonathonOH.UnityStorage.Data.json";
        public string filePath;

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
            data = GetSavedData();
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
            StreamWriter writer = new StreamWriter(filePath);
            await writer.WriteAsync(JsonConvert.SerializeObject(data));
            writer.Close();
        }

        /// <summary>
        /// Returns new dictionary if no data is saved
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, string> GetSavedData()
        {
            if (!File.Exists(filePath)) return new Dictionary<string, string>();

            StreamReader reader = new StreamReader(filePath);
            string rawContents = reader.ReadToEnd();
            reader.Close();
            Dictionary<string, string> readData = JsonConvert.DeserializeObject<Dictionary<string, string>>(rawContents);
            if (readData != null) return readData;

            return new Dictionary<string, string>();
        }
    }
}