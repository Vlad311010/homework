﻿using System.Collections;
using MatrixIndex = (int Column, int Row);

namespace t1
{
    public class SparseMatrix : IEnumerable<long>
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
            return _nonzeroElements
                .OrderBy(pair => (pair.Key.Row, pair.Key.Column))
                .Select(pair => (pair.Key.Column, pair.Key.Row, pair.Value));
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

        public IEnumerator<long> GetEnumerator()
        {
            for (int y = 0; y < _rows; y++)
            {
                for (int x = 0; x < _columns; x++)
                {
                    long value = 0;
                    _nonzeroElements.TryGetValue((x, y), out value);
                    yield return value; 
                }
            }
        }
    }
}
