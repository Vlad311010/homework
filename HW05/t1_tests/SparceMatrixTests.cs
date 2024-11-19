using t1;

namespace t1_tests
{
    [TestClass]
    public class SparceMatrixTests
    {
        int _cols = 10;
        int _rows = 10;
        SparseMatrix _matrix;

        [TestInitialize]
        public void Init()
        {
            _matrix = new SparseMatrix(_cols, _rows);

            _matrix[0, 0] = 1;
            _matrix[9, 0] = 1;
            _matrix[0, 9] = 1;
            _matrix[9, 9] = 1;
            _matrix[4, 4] = 1;

            _matrix[1, 0] = 2;
            _matrix[8, 0] = 2;
            _matrix[1, 9] = 2;
            _matrix[8, 9] = 2;
        }


        [TestMethod]
        [DataRow(0, 91)]
        [DataRow(1, 5)]
        [DataRow(2, 4)]
        public void GetCountTest(int countTarget, int result)
        {
            Assert.AreEqual(_matrix.GetCount(countTarget), result);
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
            var nonZeroValues = _matrix.GetNonzeroElements().ToArray();
            Assert.AreEqual(nonZeroValues[elementIdx].Item1, column);
            Assert.AreEqual(nonZeroValues[elementIdx].Item2, row);
            Assert.AreEqual(nonZeroValues[elementIdx].Item3, value);
        }
    }
}