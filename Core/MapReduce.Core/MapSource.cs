namespace MapReduce.Core
{
    /// <summary>
    /// ������� ������ ��� ������� map
    /// </summary>
    public class MapSource<TKey, TValue> : MapReduce.Core.KeyValuePair<TKey, TValue>
    {
        public MapSource() : base() {}

        public MapSource(TKey key, TValue value) : base(key, value) { }
    }
}