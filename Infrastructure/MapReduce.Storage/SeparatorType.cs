namespace MapReduce.Storage
{
    /// <summary>
    ///  Тип разделителя
    /// </summary>
    public enum SeparatorType
    {
        /// <summary>
        /// Разделитель строк (разделитель обозначает начало новой строки)
        /// </summary>
        RowSeparator,
        /// <summary>
        /// Разделитель значений внутри строки (разделитель обозначает начало новой колонки)
        /// </summary>
        ColumnSeparator
    }
}