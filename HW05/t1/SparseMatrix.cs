using System.Collections;

using MatrixIndex = (int column, int row);

namespace t1
{
    internal class SparseMatrix : IEnumerable<long>
    {
        int _columns;
        int _rows;

        Dictionary<MatrixIndex, long> _nonzeroElements = new Dictionary<MatrixIndex, long>();

        public SparseMatrix(int columns, int rows)
        {
            if (rows <= 0 || columns <= 0)
                throw new ArgumentException($"Invalid matrix dimensions. Columns: {columns}, rows: {rows}");

            _rows = rows;
            _columns = columns;
        }

        public long this[int column, int row]
        {
            get
            {
                long matrixValue = 0;
                _nonzeroElements.TryGetValue((column, row), out matrixValue);
                return matrixValue;
            }
            set
            {
                _nonzeroElements[(column, row)] = value;
            }
        }

        public IEnumerable<(int, int, long)> GetNonzeroElements()
        {
            return _nonzeroElements.OrderBy(pair => (pair.Key.row, pair.Key.column)).Select(pair => (pair.Key.column, pair.Key.row, pair.Value));
        }

        public int GetCount(long x)
        {
            if (x == 0)
                return _columns * _rows - _nonzeroElements.Count;

            return _nonzeroElements.Values.Count(value => value == x);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public SparseMatrixEnum GetEnumerator()
        {
            long[] elements = new long[_columns * _rows];
            for (int y = 0; y < _rows; y++)
            {
                for (int x = 0; x < _columns; x++)
                {
                    elements[x + y * _columns] = this[x, y];
                    long value = 0;
                    _nonzeroElements.TryGetValue((x, y), out value);
                }
            }
            
            return new SparseMatrixEnum(elements);
        }

        IEnumerator<long> IEnumerable<long>.GetEnumerator()
        {
            return (IEnumerator<long>)GetEnumerator();
        }
    }
}
