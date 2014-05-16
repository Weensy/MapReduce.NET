namespace MapReduce.Core
{
    /// <summary>
    /// ��������� ���� ����-��������
    /// </summary>
    /// <typeparam name="TKey">��� �����</typeparam>
    /// <typeparam name="TValue">T�� ��������</typeparam>
    public interface IKeyValuePair<TKey, TValue>
    {
        /// <summary>
        /// ����
        /// </summary>
        TKey Key { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        TValue Value { get; set; }
    }
}