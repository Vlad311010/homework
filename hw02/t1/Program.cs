namespace t1
{
    class Point
    {
        public int X { get => _point[0]; set => _point[0] = value; }
        public int Y { get => _point[1]; set => _point[1] = value; }
        public int Z { get => _point[2]; set => _point[2] = value; }
        public  float Mass { get => _mass; set => _mass = value < 0 ? 0 : value; }

        private int[] _point;
        private float _mass;

        public Point(int x, int y, int z, float mass = 0)
        {
            _point = new int[3];
            X = x;   
            Y = y;
            Z = z;
            this._mass = mass;
        }

        public bool IsZero()
        {
            return X == 0 && Y == 0 && Z == 0;
        }

        public double Distance(Point other)
        {
            return Math.Sqrt(
                (X - other.X) * (X - other.X) +
                (Y - other.Y) * (Y - other.Y) +
                (Z - other.Z) * (Z - other.Z)
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
