namespace t1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Queue<int> queue = new Queue<int>(5, false);
            //Queue<int> queue = new Queue<int>(1, true);

            Console.WriteLine(queue);
            Console.WriteLine("enqueue 1");
            queue.Enqueue(1);
            Console.WriteLine(queue);
            Console.WriteLine("enqueue 2");
            queue.Enqueue(2);
            Console.WriteLine(queue);
            Console.WriteLine("enqueue 3");
            queue.Enqueue(3);
            Console.WriteLine(queue);
            Console.WriteLine("enqueue 4");
            queue.Enqueue(4);
            Console.WriteLine(queue);
            Console.WriteLine("enqueue 5");
            queue.Enqueue(5);
            Console.WriteLine(queue);

            Console.WriteLine("Dequeue " + queue.Dequeue());
            Console.WriteLine(queue);
            Console.WriteLine("Dequeue " + queue.Dequeue());
            Console.WriteLine(queue);
            Console.WriteLine("Dequeue " + queue.Dequeue());
            Console.WriteLine(queue);
            Console.WriteLine("Dequeue " + queue.Dequeue());
            Console.WriteLine(queue);
            Console.WriteLine("Dequeue " + queue.Dequeue());
            Console.WriteLine(queue);


            Console.WriteLine("-----------------");
            /* Console.WriteLine(queue);

            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Dequeue();
            queue.Enqueue(3);
            queue.Dequeue();
            queue.Enqueue(4);
            queue.Enqueue(5);
            queue.Enqueue(6);
            queue.Dequeue();
            queue.Enqueue(7);
            queue.Enqueue(8);
            Console.WriteLine(queue);*/

            queue = new Queue<int>(4, false);
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Enqueue(4);


            Console.WriteLine("New queue " + queue);
            Console.WriteLine("Take tail " + queue.Tail());


        }
    }
}
