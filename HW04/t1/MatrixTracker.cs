namespace t1
{
    internal class MatrixTracker<T>
    {
        int _lastChangedRow = -1;
        int _lastChangedColumn = -1;
        DiagonalMatrix<T> _trackedMatrix;
        T _previousValue;


        public MatrixTracker(DiagonalMatrix<T> matrix)
        {
            _trackedMatrix = matrix;
            matrix.ElementChanged += OnElementChange;
        }

        private void OnElementChange(object? sender, MatrixEventArgs<T> e)
        {
            _lastChangedColumn = e.Column;
            _lastChangedRow = e.Row;
            _previousValue = e.oldValue;
        }

        public void Undo()
        {
            if (_lastChangedRow < 0 || _lastChangedColumn < 0)
            {
                throw new InvalidOperationException("No changes has been done to the tracked matrix");
            }

            _trackedMatrix[_lastChangedColumn, _lastChangedRow] = _previousValue;
        }
    }
}
