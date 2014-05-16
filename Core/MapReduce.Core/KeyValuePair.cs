using System;


namespace MapReduce.Core
{
    /// <summary>
    /// Пара ключ-значение
    /// </summary>
    /// <typeparam name="TKey">Тип ключа</typeparam>
    /// <typeparam name="TValue">Tип значения</typeparam>
    [Serializable]
    public struct KeyValuePair<TKey, TValue> : IKeyValuePair<TKey, TValue>
    {
        public KeyValuePair(TKey key, TValue value) : this()
        {
            Key = key;
            Value = value;
        }

        /// <summary>
        /// Ключ 
        /// </summary>
        public TKey Key { get; set; }

        /// <summary>
        /// Значение
        /// </summary>
        public TValue Value { get; set; }
    }
}