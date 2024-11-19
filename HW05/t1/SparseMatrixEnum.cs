using System.Collections;

namespace t1
{
    public class SparseMatrixEnum : IEnumerator
    {
        public long[] _values;
        int _currentIdx = -1;

        public SparseMatrixEnum(long[] values)
        {
            _values = values;
        }
        public bool MoveNext()
        {
            _currentIdx++;
            return _currentIdx < _values.Length;
        }
            
        public void Reset()
        {
            _currentIdx = -1;
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public long Current
        {
            get
            {
                try
                {
                    return _values[_currentIdx];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}
