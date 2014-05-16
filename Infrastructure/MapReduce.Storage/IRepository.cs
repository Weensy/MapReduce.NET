using System.Collections.Generic;

namespace MapReduce.Storage
{
    /// <summary>
    /// Интерфейс хранилища данных
    /// </summary>
    /// <typeparam name="TEntity">Тип элементов в хранилище данных</typeparam>
    public interface IRepository<out TEntity>
    {
        IEnumerable<TEntity> Get();
    }
}