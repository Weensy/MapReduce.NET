using System.Collections.Generic;

namespace MapReduce.Analysis.Finance
{
    /// <summary>
    /// �������=��� �������� � ������
    /// </summary>
    /// <typeparam name="TTarget">��� ������������� �������</typeparam>
    public interface IMappingStrategy<out TTarget>
    {
        TTarget Map(string source);
    }
}