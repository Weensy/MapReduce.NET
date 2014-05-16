namespace MapReduce.Core
{
    /// <summary>
    /// ��������� ��������� ������� reduce
    /// </summary>
    public class ReduceResult<TKey, TValue> : MapReduce.Core.KeyValuePair<TKey, TValue>
    {
        public ReduceResult(TKey key, TValue value) : base(key, value) { }
    }
}