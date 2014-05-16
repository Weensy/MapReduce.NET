using System;


namespace MapReduce.Core
{
    /// <summary>
    /// ���� ����-��������
    /// </summary>
    /// <typeparam name="TKey">��� �����</typeparam>
    /// <typeparam name="TValue">T�� ��������</typeparam>
    [Serializable]
    public struct KeyValuePair<TKey, TValue> : IKeyValuePair<TKey, TValue>
    {
        public KeyValuePair(TKey key, TValue value) : this()
        {
            Key = key;
            Value = value;
        }

        /// <summary>
        /// ���� 
        /// </summary>
        public TKey Key { get; set; }

        /// <summary>
        /// ��������
        /// </summary>
        public TValue Value { get; set; }
    }
}