using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using MapReduce.Storage;
using MapReduce.Storage.Extensions;

namespace MapReduce.Analysis.Finance
{
    /// <summary>
    /// —тратеги€ преобразовани€ дл€ архивов торгов, предоставл€емых Finam
    /// </summary>
    /// <remarks>
    /// ќжидаемый формат: ticker;per;date;time;open;high;low;close;vol
    /// </remarks>
    public class QuoteMappingStrategy : IMappingStrategy<Quote>
    {
        private readonly char _columnSeparator = SeparatorType.ColumnSeparator.ToChar();

        private const bool IgnoreCase = true;

        public IEnumerable<string> GroupByKeys
        {
            get { return new[] { "Ticker" }; }
        }


        public Quote Map(string source)
        {
            Contract.Requires<ArgumentNullException>(source != null);

            Quote quote = default(Quote);

            if (source.Split(_columnSeparator).Count() == 9)
            {
                quote = new Quote(IgnoreCase, GroupByKeys.ToArray())
                {
                    Ticker = source.ToString(1, _columnSeparator),
                    Open = source.ToDecimal(5, _columnSeparator),
                    Low = source.ToDecimal(7, _columnSeparator),
                    High = source.ToDecimal(6, _columnSeparator),
                    Close = source.ToDecimal(8, _columnSeparator),
                    Volume = source.ToInt64(9, _columnSeparator),
                };
            }
            else
            {
                quote = new Quote(IgnoreCase, GroupByKeys.ToArray()) { Ticker = "unknown" };
            }

            return quote;
        }
    }
}