namespace t1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int cols = 10;
            int rows = 10;
            SparseMatrix m = new SparseMatrix(cols, rows);

            m[0,0] = 1;
            m[9, 0] = 1;
            m[0, 9] = 1;
            m[9, 9] = 1;
            
            m[1, 0] = 2;
            m[8, 0] = 2;
            m[1, 9] = 2;
            m[8, 9] = 2;

            Console.WriteLine("Count 0: " + m.GetCount(1));
            Console.WriteLine("Count 1: " + m.GetCount(1));

            Console.WriteLine("Non-zero elements:");
            foreach (var item in m.GetNonzeroElements())
            {
                Console.WriteLine($"[{item.Item1}, {item.Item2}] = {item.Item3}");
            }


            Console.WriteLine("All values:");
            int counter = 0;
            foreach (var val in m)
            {
                Console.Write(val + " ");
                counter++;
                if (counter == cols)
                {
                    counter %= cols;
                    Console.Write("\n");
                }
            }
        }
    }
}
