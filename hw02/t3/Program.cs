namespace t3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string a = "AS";
            string b = string.Copy(a);
            Console.WriteLine(a);
            Console.WriteLine(b);
            Console.WriteLine(ReferenceEquals(a, b));
        }
    }
}
