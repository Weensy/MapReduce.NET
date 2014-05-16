namespace MapReduce.Storage
{
    /// <summary>
    /// Блок текстовых данных
    /// </summary>
    public class TextBlock : MapReduce.Storage.DataBlock
    {
        public TextBlock(string id, string content)
        {
            Id = id;
            Content = content;
        }

        /// <summary>
        /// Содержимое текстового блока
        /// </summary>
        public string Content { get; set; }
    }
}