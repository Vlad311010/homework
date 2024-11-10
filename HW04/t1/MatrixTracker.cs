namespace t1
{
    internal class MatrixTracker<T> : IDisposable
    {
        private DiagonalMatrix<T> _trackedMatrix;
        private Stack<ValueTuple<int, int, T>> _changesStack = new Stack<ValueTuple<int, int, T>>();


        public MatrixTracker(DiagonalMatrix<T> matrix)
        {
            _trackedMatrix = matrix;
            matrix.ElementChanged += OnElementChange;
        }

        private void OnElementChange(object? sender, MatrixEventArgs<T> e)
        {
            _changesStack.Push((e.Column, e.Row, e.oldValue));
        }

        public void Undo()
        {
            if (_changesStack.Count == 0)
            {
                throw new InvalidOperationException("No changes has been done to the tracked matrix");
            }

            (int column, int row, T previousValue) = _changesStack.Pop();
            
            _trackedMatrix.ElementChanged -= OnElementChange;
            _trackedMatrix[column, row] = previousValue;
            _trackedMatrix.ElementChanged += OnElementChange;
        }

        public void Dispose()
        {
            // unsubscribe event to avoid possible memory leak
            _trackedMatrix.ElementChanged -= OnElementChange;
        }
    }
}
