using System.Collections.Generic;


namespace MapReduce.Storage
{
    /// <summary>
    /// Обобщенная реализация хранилища данных
    /// </summary>
    /// <typeparam name="TEntity">Тип элементов в хранилище данных</typeparam>
    public abstract class BaseRepository<TEntity> :
        IRepository<TEntity>
        where TEntity : class
    {
        public abstract IEnumerable<TEntity> Get();
    }
}