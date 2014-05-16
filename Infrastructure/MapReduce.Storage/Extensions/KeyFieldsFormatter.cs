namespace MapReduce.Storage.Extensions
{
    using System;
    using System.Diagnostics.Contracts;


    public static class KeyFieldsFormatter
    {
        public static string Format(string propertyName, string propertyValue)
        {
            Contract.Requires<ArgumentNullException>(propertyName != null);

            return String.Format("{0}={1}&", propertyName, propertyValue);
        }
    }
}