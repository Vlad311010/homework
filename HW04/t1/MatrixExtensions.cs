
namespace t1
{
    public static class MatrixExtensions
    {
        public static void Add<T>(this DiagonalMatrix<T> matrix, DiagonalMatrix<T> other, Func<T, T, T> sum)
        {
            if (matrix.Size != other.Size)
                throw new ArgumentException($"Can't add matrixes of a different size. matrix.Size: {matrix.Size}, other.Size:{other.Size}");

            T[] newMatrixValues = new T[matrix.Size];

            for (int i = 0; i < matrix.Size; i++)
            {
                T matrixValue = i < matrix.Size ? matrix[i, i] : default!;
                T otherValue = i < other.Size ? other[i, i] : default!;
                matrix[i, i] = sum(matrixValue, otherValue);
            }

        }
    }
}
