namespace MapReduce.Storage.Configurations
{
    /// <summary>
    /// Конфигурация хранилища
    /// </summary>
    public class StorageConfiguration
    {
        /// <summary>
        /// Путь до папки с входными данными
        /// </summary>
        public string InputFolder { get; set; }

        /// <summary>
        /// Путь до папки с выходными данными
        /// </summary>
        public string OutputFolder { get; set; }
    }
}
