using System;
using System.Text;

namespace t2
{

    public class DiagonalMatrix
    {
        public int Size => elements.Length;

        private int[] elements;

        public DiagonalMatrix(params int[] elements)
        {
            if (elements == null)
            {
                elements = Array.Empty<int>();
                return;
            }

            this.elements = elements;
        }

        public int this[int i, int j]
        {
            get 
            {
                if (i != j || i < 0 || i >= elements.Length)
                    return 0;
                else 
                    return elements[i];
            }
            set
            {
                if (i != j || i < 0 || i >= elements.Length)
                    return;
                else
                    elements[i] = value;
            }
        }

        public int Track()
        {
            int acc = 0;
            for (int i = 0; i < elements.Length; i++)
            {
                acc += elements[i];
            }
            return acc;
        }

        public override string ToString() 
        { 
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < elements.Length; i++)
            {
                sb.Append("[");
                sb.Append(string.Concat(Enumerable.Repeat("0, ", i)));
                sb.Append(elements[i].ToString());
                sb.Append(string.Concat(Enumerable.Repeat(", 0", elements.Length - 1 - i)));
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
                if (other[i, i] != this[i, i])
                    return false;
            }

            return true;
        }
    }

    public static class MatrixExtensions
    {
        public static DiagonalMatrix Add(this DiagonalMatrix matrix, DiagonalMatrix other)
        {
            int newMatrixSize = Math.Max(matrix.Size, other.Size);
            int[] newMatrixValues = new int[newMatrixSize];

            for (int i = 0; i < newMatrixSize; i++)
            {
                int matrixValue = i < matrix.Size ? matrix[i, i] : 0;
                int otherValue = i < other.Size ? other[i, i] : 0;
                newMatrixValues[i] = matrixValue + otherValue;
            }

            return new DiagonalMatrix(newMatrixValues);
            
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var a = new DiagonalMatrix(1, 2);
            var b = new DiagonalMatrix(0, 0, 0, 6);

            
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
