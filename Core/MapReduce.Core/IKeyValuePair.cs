namespace MapReduce.Core
{
    /// <summary>
    /// Интерфейс пары ключ-значение
    /// </summary>
    /// <typeparam name="TKey">Тип ключа</typeparam>
    /// <typeparam name="TValue">Tип значения</typeparam>
    public interface IKeyValuePair<TKey, TValue>
    {
        /// <summary>
        /// Ключ
        /// </summary>
        TKey Key { get; set; }

        /// <summary>
        /// Значение
        /// </summary>
        TValue Value { get; set; }
    }
}