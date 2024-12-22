using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace t2
{
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
}
