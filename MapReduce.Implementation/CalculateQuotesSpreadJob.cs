using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.Contracts;


namespace MapReduce.Analysis.Finance
{
    using Storage;
    using Storage.Extensions;


    /// <summary>
    /// Задача расчета спреда актива
    /// </summary>
    public class CalculateQuotesSpreadJob :
        MapReduce.Core.GenericMapReduce<MapReduce.Core.KeyValuePair<String, String>, String, Tuple<decimal, decimal>, MapReduce.Core.KeyValuePair<String, Decimal>>
    {
        public CalculateQuotesSpreadJob(IMappingStrategy<Quote> mappingStrategy) : base()
        {
            Contract.Requires<ArgumentNullException>(mappingStrategy != null);
            _mappingStrategy = mappingStrategy;
        }

        private readonly IMappingStrategy<Quote> _mappingStrategy;


        public override IEnumerable<MapReduce.Core.KeyValuePair<String, Tuple<decimal, decimal>>> Map(MapReduce.Core.KeyValuePair<String, String> values)
        {
            // получаем список строк в файле данных
            IEnumerable<string> rows = values.Value.Split(SeparatorType.RowSeparator.ToChar());
            // получаем список котировок
            IEnumerable<Quote> quotes = rows.Select(r => _mappingStrategy.Map(r));

            return quotes.Select(q => new Core.KeyValuePair<string, Tuple<decimal, decimal>>(q.Ticker, Tuple.Create(q.High, q.Low)));
        }

        public override MapReduce.Core.KeyValuePair<String, Decimal> Reduce(MapReduce.Core.KeyValuePair<String, IEnumerable<Tuple<decimal, decimal>>> value)
        {
            return new MapReduce.Core.KeyValuePair<string, decimal>(
               value.Key,
               value.Value
                    .Select(v => v.Item1 - v.Item2) /* считаем спред */
                    .Max()); /* ищем максимальное значение спреда */
        }
    }
}