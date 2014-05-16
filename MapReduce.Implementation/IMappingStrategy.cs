using System.Collections.Generic;

namespace MapReduce.Analysis.Finance
{
    /// <summary>
    /// Стратен=гия маппинга в объект
    /// </summary>
    /// <typeparam name="TTarget">Тип возвращаемого объекта</typeparam>
    public interface IMappingStrategy<out TTarget>
    {
        TTarget Map(string source);
    }
}