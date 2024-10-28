using System;
using System.Text;

namespace t2
{

    public class DiagonalMatrix
    {
        public int Size => _elements.Length;

        private int[] _elements;

        public DiagonalMatrix(params int[] elements)
        {
            if (elements == null)
            {
                elements = Array.Empty<int>();
                return;
            }

            this._elements = new int[elements.Length];
            Array.Copy(elements, this._elements, elements.Length);
        }

        public int this[int i, int j]
        {
            get 
            {
                return IsIndexValid(i, j) ? _elements[i] : 0;
            }
            set
            {
                if (IsIndexValid(i, j))
                    _elements[i] = value;
            }
        }

        private bool IsIndexValid(int i, int j)
        {
            return i == j && i >= 0 && i < _elements.Length;
        }

        public int Track()
        {
            int acc = 0;
            for (int i = 0; i < _elements.Length; i++)
            {
                acc += _elements[i];
            }
            return acc;
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

        public override bool Equals(object? obj)
        {
            DiagonalMatrix? other = obj as DiagonalMatrix;
            if (other == null || other.Size != this.Size)
                return false;
            
            for (int i = 0; i < this.Size; i++)
            {
                if (this._elements[i] != other._elements[i])
                    return false;
            }

            return true;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var a = new DiagonalMatrix(1, 2);
            var b = new DiagonalMatrix(0, 0, 0, 6);

            a[0, 0] = 2;
            a[0, 1] = 2;

            Console.Write("a:\n" + a);
            Console.WriteLine("Track a = " + a.Track());
            Console.Write("\nb:\n" + b);
            Console.WriteLine("Track b = " + b.Track());
            var sumMatrix = a.Add(b);
            Console.Write("\na + b:\n" + sumMatrix);   
            Console.WriteLine("Track = " + sumMatrix.Track());
        }
    }
}
