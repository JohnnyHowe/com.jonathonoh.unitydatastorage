namespace JonathonOH.UnityDataStorage
{
    public interface IDataPersistor
    {
        T TryGet<T>(string key, T fallback);

        void Set<T>(string key, T value, bool saveNow = true);

        /// <summary>
        /// Is there any value stored with the given key?
        /// No type guarantee
        /// </summary>
        bool Contains(string key);

        void Save();

        /// <summary>
        /// Is there any persisted data at all?
        /// </summary>
        bool SavedDataExists();
    }
}