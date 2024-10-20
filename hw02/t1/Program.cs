namespace t1
{
    class Point
    {
        public int X { get => point[0]; set => point[0] = value; }
        public int Y { get => point[1]; set => point[1] = value; }
        public int Z { get => point[2]; set => point[2] = value; }
        public  float Mass { get => mass; set => mass = value < 0 ? 0 : value; }

        private int[] point;
        private float mass;

        public Point(int x, int y, int z, float mass = 0)
        {
            point = new int[3];
            point[0] = x;   
            point[1] = y;
            point[2] = z;
            this.mass = mass;
        }

        public bool IsZero()
        {
            return point[0] == 0 && point[1] == 0 && point[2] == 0;
        }

        public double Distance(Point other)
        {
            return Math.Sqrt(
                (point[0] - other.point[0]) * (point[0] - other.point[0]) +
                (point[1] - other.point[1]) * (point[1] - other.point[1]) +
                (point[2] - other.point[2]) * (point[2] - other.point[2])
            );
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Point a = new Point(0, 0, 5, 10);
            Point b = new Point(0, 0, 0, 0);

            Console.WriteLine("Point a coordinates: " + $"({a.X}, {a.Y}, {a.Z}); Is zero? {a.IsZero()}");
            Console.WriteLine("Point b coordinates: " + $"({b.X}, {b.Y}, {b.Z}); Is zero? {b.IsZero()}");
            Console.WriteLine("Point a mass: " + a.Mass);
            Console.WriteLine("Point b mass: " + b.Mass);
            Console.WriteLine("Distance betweeen a and b: " + a.Distance(b));
            b.Z = 5;
            Console.WriteLine("Set b.Z to 5");
            Console.WriteLine("Point b coordinates: " + $"({b.X}, {b.Y}, {b.Z}); Is zero? {b.IsZero()}");
            Console.WriteLine("New distance betweeen a and b: " + a.Distance(b));


        }
    }
}
