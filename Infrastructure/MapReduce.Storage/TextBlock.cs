namespace MapReduce.Storage
{
    /// <summary>
    /// ���� ��������� ������
    /// </summary>
    public class TextBlock : MapReduce.Storage.DataBlock
    {
        public TextBlock(string id, string content)
        {
            Id = id;
            Content = content;
        }

        /// <summary>
        /// ���������� ���������� �����
        /// </summary>
        public string Content { get; set; }
    }
}