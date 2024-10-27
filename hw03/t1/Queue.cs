using System.Text;

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

    // array based implementation
    internal class Queue<T> : IQueue<T> where T : struct
    {
        private T[] _elements;
        private int _elementsCount;
        private int _start = 0;
        private int _end = 0;
        private bool _autoResize = false;
        public Queue(int size, bool autoResize = false)
        {
            _elements = new T[size];
            _autoResize = autoResize;
        }


        public void Enqueue(T element)
        {
            if (_elementsCount == _elements.Length)
            {
                if (_autoResize)
                {
                    int currentElementsCount = _elementsCount;
                    // store queque values in temporary array 
                    T[] tmpArray = new T[currentElementsCount];
                    int pointer = _start;
                    for (int i = 0; i < currentElementsCount; i++)
                    {
                        tmpArray[i] = _elements[pointer];
                        IncrementIndex(ref pointer);
                    }

                    // resize queue
                    Array.Resize(ref _elements, _elements.Length * 2); 
                    
                    // set start and end pointers and repopulate array
                    _start = 0;
                    _end = currentElementsCount;
                    for (int i = 0; i < currentElementsCount; i++)
                    {
                        _elements[i] = tmpArray[i];
                    }
                }
                else
                {
                    throw new InvalidOperationException("Queue is full");
                }
            }

            _elementsCount++;
            _elements[_end] = element;
            IncrementIndex(ref _end);
        }
        
        public T Dequeue()
        {
            if (IsEmpty())
                throw new InvalidOperationException("Queue is empty");

            T head = _elements[_start];
            _elementsCount--;
            IncrementIndex(ref _start);
            
            return head;
        }

        private void IncrementIndex(ref int index)
        {
            index = (index + 1) % _elements.Length;
        }

        public bool IsEmpty()
        {
            return _elementsCount == 0; 
        }

        public int Size()
        {
            return _elementsCount;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");

            int pointer = _start;
            for (int i = 0; i < _elementsCount; i++)
            {
                string separator = i == _elementsCount - 1 ? string.Empty : ", ";
                sb.Append($"{_elements[pointer].ToString()}{separator}");
                IncrementIndex(ref pointer);
            }
            sb.Append("]");
            return sb.ToString();
        }
    }
}
