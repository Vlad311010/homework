using System;

namespace t2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Fraction a = new Fraction(1, 2);
            Console.WriteLine("a: " + a.ToString());
            a = new Fraction(1* 6, 2 * 6);
            Console.WriteLine("a: " + a.ToString());

            Fraction b = new Fraction(1, 4);
            Console.WriteLine("b: " + b.ToString());
            Console.WriteLine("a - b: " + (a + b).ToString());
            Console.WriteLine("a + b: " + (a - b).ToString());
            Console.WriteLine("b + 1: " + (b + 1) + " = " + (double)(b + 1));
        }
    }
}
