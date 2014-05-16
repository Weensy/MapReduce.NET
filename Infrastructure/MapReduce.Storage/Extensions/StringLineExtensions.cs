namespace MapReduce.Storage.Extensions
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Globalization;
    using System.Linq;


    public static class StringLineExtensions
    {
        private static TTarget GetValue<TTarget>(string source, int rowOrdinalNumber, char separator, Func<string, TTarget> convertor)
        {
            Contract.Requires<ArgumentNullException>(source != null);
            Contract.Requires<ArgumentException>(rowOrdinalNumber > 0);
            Contract.Requires<ArgumentException>(convertor != null);

            var values = source.Split(separator);

            if (values.Count() < rowOrdinalNumber - 1)
                throw new ArgumentOutOfRangeException("rowOrdinalNumber");

            return convertor(values[rowOrdinalNumber - 1]);
        }


        public static string ToString(this string source, int rowOrdinalNumber, char separator)
        {
            return GetValue(source, rowOrdinalNumber, separator, s => s);
        }


        public static int ToInt32(this string source, int rowOrdinalNumber, char separator)
        {
            return GetValue(source, rowOrdinalNumber, separator, Int32.Parse);
        }


        public static long ToInt64(this string source, int rowOrdinalNumber, char separator)
        {
            return GetValue(source, rowOrdinalNumber, separator, Int64.Parse);
        }

        public static decimal ToDecimal(this string source, int rowOrdinalNumber, char separator)
        {
            return GetValue(source, rowOrdinalNumber, separator, s => Decimal.Parse(s, new NumberFormatInfo { CurrencyDecimalSeparator = "." }));
        }


        public static DateTime ToDateTime(this string source, int dateRowOrdinalNumber, int timeRowOrdinalNumber, char separator)
        {
            string sDate = source.ToString(dateRowOrdinalNumber, separator);
            string sTime = source.ToString(timeRowOrdinalNumber, separator);

            return DateTime.ParseExact(sDate, "yyyyMMdd", DefaultCultureInfo);
        }


        private static readonly CultureInfo DefaultCultureInfo = new CultureInfo("en-US");
    }
}