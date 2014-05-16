using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace MapReduce.Analysis.Finance
{
    using Storage.Extensions;


    public class Quote : System.IEquatable<Quote>
    {
        #region Ctor
        private readonly IEnumerable<String> _groupByKeys;
        private readonly bool _ignoreCase;

        protected Quote() : base() { }

        public Quote(bool ignoreCase = true, params string[] groupByKeys) : base()
        {
            _groupByKeys = groupByKeys;
            _ignoreCase = ignoreCase;
        }
        #endregion


        #region Properties
        /// <summary>
        /// Биржевой код
        /// </summary>
        public string Ticker { get; set; }

        /// <summary>
        /// Дата
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Высшая цена
        /// </summary>
        public decimal High { get; set; }

        /// <summary>
        /// Низшая цена
        /// </summary>
        public decimal Low { get; set; }

        /// <summary>
        /// Цена открытия
        /// </summary>
        public decimal Open { get; set; }

        /// <summary>
        /// Цена закрытия
        /// </summary>
        public decimal Close { get; set; }

        /// <summary>
        /// Объем
        /// </summary>
        public long Volume { get; set; }
        #endregion


        #region Comparison logic 
        public override int GetHashCode()
        {
            var hash = new StringBuilder();
            foreach (string propertyName in _groupByKeys)
                hash.Append(KeyFieldsFormatter.Format(propertyName, this.GetPropertyValue<Quote, string>(propertyName)));

            string sHash = hash.ToString();
            if (_ignoreCase)
                sHash = sHash.ToLowerInvariant();

            return sHash.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Quote)) return false;

            return Equals((Quote)obj);
        }

        public bool Equals(Quote other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return this.AreEqual(other, propertyNames: _groupByKeys.ToArray());
        }

        public static bool operator == (Quote left, Quote right)
        {
            return Equals(left, right);
        }

        public static bool operator != (Quote left, Quote right)
        {
            return !Equals(left, right);
        }
        #endregion


        #region Domain section
        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(_groupByKeys != null);
            Contract.Invariant(_groupByKeys.Any());
            Contract.Invariant(Contract.ForAll(_groupByKeys, key => key != String.Empty));
        }
        #endregion 
    }
}