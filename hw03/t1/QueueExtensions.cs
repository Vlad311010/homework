namespace t1
{
    internal static class QueueExtensions
    {
        public static IQueue<T> Tail<T>(this IQueue<T> queue) where T : struct
        {
            int tailQueueSize = queue.Size() - 1;
            Queue<T> tailQueue = new Queue<T>(tailQueueSize);
            T head = queue.Dequeue();
            queue.Enqueue(head);

            for (int i = 0; i < tailQueueSize; i++)
            {
                T element = queue.Dequeue();
                tailQueue.Enqueue(element);
                queue.Enqueue(element);
            }

            return tailQueue;
        }
    }
}
