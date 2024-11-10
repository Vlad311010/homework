namespace t1
{

    internal class Program
    {
        static void Main(string[] args)
        {
            var m1 = new DiagonalMatrix<int>(1, 2, 3);
            var m2 = new DiagonalMatrix<int>(1, 1, 1);
            var tracker = new MatrixTracker<int>(m1); 
            m1[0, 0] = 2;
            m1[0, 0] = 3;
            m1[0, 0] = 4;
            m1[0, 0] = 5;

            Console.WriteLine(m1.ToString());
            tracker.Undo();
            m1[0, 0] = 0;
            tracker.Undo();
            Console.WriteLine(m1.ToString());
            m1.Add(m2, (a,b) => a + b);
            Console.WriteLine(m1.ToString());
        }
    }
}
