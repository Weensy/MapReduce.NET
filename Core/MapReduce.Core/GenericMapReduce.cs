using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;


namespace MapReduce.Core
{
    /// <summary>
    /// Обобщенная реализация MapReduce
    /// </summary>
    /// <typeparam name="TSource">Тип элементов исходной коллекции</typeparam>
    /// <typeparam name="TKey">Типа ключа, используемого в функциях map() и reduce()</typeparam>
    /// <typeparam name="TValue">Тип значения, ассоциированного с ключом и используемого в функциях map() и reduce()</typeparam>
    /// <typeparam name="TResult">Тип элементов результирующей коллекции</typeparam>
    public abstract class GenericMapReduce<TSource, TKey, TValue, TResult>
    {
        /// <summary>
        /// Возвращает промежутной список пар, являющийся результатом обработки переданной коллекции
        /// </summary>
        /// <param name="values">Элементы исходной коллекции</param>
        /// <returns>Промежуточный список</returns>
        public abstract IEnumerable<MapReduce.Core.KeyValuePair<TKey, TValue>> Map(TSource values);

        /// <summary>
        /// Возращает коллекцию, являющуюся результатом свёртки переданного списка
        /// </summary>
        /// <param name="value">Список промежуточных значений</param>
        /// <returns>Результирующая коллекция</returns>
        public abstract TResult Reduce(MapReduce.Core.KeyValuePair<TKey, IEnumerable<TValue>> value);
        

        /// <summary>
        /// Запуск выполнения расчетов MapReduce
        /// </summary>
        /// <param name="source">Элементы исходной коллекции</param>
        /// <param name="map">Функция Map</param>
        /// <param name="reduce">Функция Reduce</param>
        public IEnumerable<TResult> MapReduce(IEnumerable<TSource> source,
                                              Func<TSource, IEnumerable<MapReduce.Core.KeyValuePair<TKey, TValue>>> map,
                                              Func<KeyValuePair<TKey, IEnumerable<TValue>>, TResult> reduce)
        {
            Contract.Requires<ArgumentNullException>(source != null);
            Contract.Requires<ArgumentNullException>(map != null);
            Contract.Requires<ArgumentNullException>(reduce != null);

            // map phase
            var mapResults = new ConcurrentBag<KeyValuePair<TKey, TValue>>();

            Parallel.ForEach(
                source,
                item => 
                {
                    foreach (var result in map(item))
                        mapResults.Add(result);
                });

            // reduce phase
            var reduceSources = mapResults
                                    .GroupBy(
                                        item =>
                                        item.Key,
                                        (key, values) => new KeyValuePair<TKey, IEnumerable<TValue>>(key, values.Select(value => value.Value)));

            var reduceResult = new ConcurrentBag<TResult>();

            Parallel.ForEach(
                        reduceSources,
                        item => reduceResult.Add(reduce(item)));

            // return result
            return reduceResult;
        }
    }
}