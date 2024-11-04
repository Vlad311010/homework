namespace t1
{
    public class MatrixEventArgs<T> : EventArgs
    {
        public int Row;
        public int Column;
        public T oldValue;
        public T newValue;
    }
}
