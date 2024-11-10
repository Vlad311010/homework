using System.Text;

namespace t1
{
    public class DiagonalMatrix<T>
    {
        public int Size => _elements.Length;

        private T[] _elements;

        public event EventHandler<MatrixEventArgs<T>> ElementChanged;

        public DiagonalMatrix(int size)
        {
            if (size < 0)
                throw new ArgumentException($"Can't create matrix of a size {size}. Matrix size can't be negative.");

            _elements = new T[size];
        }

        public DiagonalMatrix(params T[] elements)
        {
            if (elements == null)
            {
                elements = Array.Empty<T>();
                return;
            }


            _elements = new T[elements.Length];
            Array.Copy(elements, _elements, elements.Length);
        }

        public T this[int i, int j]
        {
            get
            {
                if (IsIndexValid(i, j))
                    return IsDiagonalIndex(i, j) ? _elements[i] : default!;
                
                throw new IndexOutOfRangeException($"Invalid index [{i},{j}]");
            }
            set
            {
                if (IsIndexValid(i, j) && IsDiagonalIndex(i, j))
                {
                    if (!_elements[i].Equals(value))
                    {
                        if (ElementChanged != null)
                            ElementChanged(this, new MatrixEventArgs<T>() { Column = i, Row = j, oldValue = _elements[i], newValue = value });

                        _elements[i] = value;
                    }
                }
                else
                    throw new IndexOutOfRangeException($"Invalid index [{i},{j}]");
            }
        }

        private bool IsDiagonalIndex(int i, int j)
        {
            return i == j;
        }

        private bool IsIndexValid(int i, int j)
        {
            return IsDiagonalIndex(i, j) && i >= 0 && i < _elements.Length;
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < _elements.Length; i++)
            {
                sb.Append("[");
                sb.Append(string.Concat(Enumerable.Repeat("0, ", i)));
                sb.Append(_elements[i].ToString());
                sb.Append(string.Concat(Enumerable.Repeat(", 0", _elements.Length - 1 - i)));
                sb.Append("]\n");
            }
            return sb.ToString();
        }
    }
}
