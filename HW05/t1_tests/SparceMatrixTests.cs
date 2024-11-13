using t1;

namespace t1_tests
{
    [TestClass]
    public class SparceMatrixTests
    {
        int cols = 10;
        int rows = 10;
        SparseMatrix matrix;

        [TestInitialize]
        public void Init()
        {
            matrix = new SparseMatrix(cols, rows);

            matrix[0, 0] = 1;
            matrix[9, 0] = 1;
            matrix[0, 9] = 1;
            matrix[9, 9] = 1;
            matrix[4, 4] = 1;

            matrix[1, 0] = 2;
            matrix[8, 0] = 2;
            matrix[1, 9] = 2;
            matrix[8, 9] = 2;
        }


        [TestMethod]
        [DataRow(0, 91)]
        [DataRow(1, 5)]
        [DataRow(2, 4)]
        public void GetCountTest(int countTarget, int result)
        {
            Assert.AreEqual(matrix.GetCount(countTarget), result);
        }
        
        [TestMethod]
        [DataRow(0, 0, 0, 1)]
        [DataRow(1, 1, 0, 2)]
        [DataRow(2, 8, 0, 2)]
        [DataRow(3, 9, 0, 1)]
        [DataRow(4, 4, 4, 1)]
        [DataRow(5, 0, 9, 1)]
        [DataRow(6, 1, 9, 2)]
        [DataRow(7, 8, 9, 2)]
        [DataRow(8, 9, 9, 1)]
        public void GetNonzeroElements(int elementIdx, int column, int row, int value)
        {
            var nonZeroValues = matrix.GetNonzeroElements().ToArray();
            Assert.AreEqual(nonZeroValues[elementIdx].Item1, column);
            Assert.AreEqual(nonZeroValues[elementIdx].Item2, row);
            Assert.AreEqual(nonZeroValues[elementIdx].Item3, value);
        }
    }
}