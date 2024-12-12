namespace t1
{
    internal interface IQueue<T> where T : struct
    {
        public void Enqueue(T element);
        public T Dequeue();
        public bool IsEmpty();

        // additional function that returns number of elements in queue
        public int Size();
    }
}
