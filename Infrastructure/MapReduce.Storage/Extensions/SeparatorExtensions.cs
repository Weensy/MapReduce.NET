namespace MapReduce.Storage.Extensions
{
    using System;


    public static class SeparatorExtensions
    {
        public static char ToChar(this SeparatorType separatorType, SeparatorScheme separatorScheme = SeparatorScheme.Default)
        {
            char @char = default(char);

            switch(separatorScheme)
            {
                case SeparatorScheme.Default:
                    switch (separatorType)
                    {
                        case SeparatorType.RowSeparator:
                            @char = '\n'; // new line
                            break;
                        case SeparatorType.ColumnSeparator:
                            @char = ';';
                            break;
                        default:
                            throw new ArgumentOutOfRangeException("separatorType", "Not supported separator type");
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException("separatorScheme", "Not supported separator scheme");
            }
            
            return @char;
        }
    }
}
