namespace MapReduce.Storage.Extensions
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Text;


    public static class MapReduceItemExtensions
    {
        public static TProperty GetPropertyValue<TClass, TProperty>(this IEquatable<TClass> source, string propertyName)
            where TClass : class
        {
            Contract.Requires<ArgumentNullException>(source != null);
            Contract.Requires<ArgumentNullException>(propertyName != null);

            return (TProperty)typeof(TClass).GetProperty(propertyName).GetValue(source, null);
        }


        public static bool AreEqual<TClass>(this IEquatable<TClass> left, IEquatable<TClass> right, StringComparison comparison = StringComparison.InvariantCultureIgnoreCase, params string[] propertyNames)
            where TClass : class
        {
            Contract.Requires<ArgumentNullException>(left != null);
            Contract.Requires<ArgumentNullException>(right != null);
            Contract.Requires<ArgumentNullException>(propertyNames != null);

            var leftPropertyValue = new StringBuilder();
            var rightPropertyValue = new StringBuilder();

            foreach (string propertyName in propertyNames)
            {
                leftPropertyValue.Append(KeyFieldsFormatter.Format(propertyName, left.GetPropertyValue<TClass, String>(propertyName)));
                rightPropertyValue.Append(KeyFieldsFormatter.Format(propertyName, right.GetPropertyValue<TClass, String>(propertyName)));
            }

            return leftPropertyValue.ToString().Equals(rightPropertyValue.ToString(), comparison);
        }
    }
}