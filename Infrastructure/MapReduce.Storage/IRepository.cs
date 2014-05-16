using System.Collections.Generic;

namespace MapReduce.Storage
{
    /// <summary>
    /// ��������� ��������� ������
    /// </summary>
    /// <typeparam name="TEntity">��� ��������� � ��������� ������</typeparam>
    public interface IRepository<out TEntity>
    {
        IEnumerable<TEntity> Get();
    }
}