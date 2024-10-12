using System.Text;

namespace t1
{
    internal class Program
    {
        private static void ListAADigits(int a, int b)
        {
            if (b < 130)
                return;

            StringBuilder stringBuilder = new StringBuilder();
            for (int i = a; i <= b; i++)
            {
                int decimalNumber = i;
                int aCounter = 0;
                while (decimalNumber > 0)
                {
                    int remainder = decimalNumber % 12;
                    decimalNumber = decimalNumber / 12;
                    switch (remainder)
                    {
                        case 10:
                            stringBuilder.Insert(0, 'A');
                            aCounter++;
                            break;
                        case 11:
                            stringBuilder.Insert(0, 'B');
                            break;
                        default:
                            stringBuilder.Insert(0, remainder);
                            break;
                    }
                }

                if (aCounter == 2)
                    Console.WriteLine(stringBuilder.ToString());

                stringBuilder.Clear();
            }

        }


        private static void Solution()
        {
            Console.WriteLine("Enter two integers separeted by space");
            string[] input = Console.ReadLine()!.Split(' ');
            int a = int.Parse(input[0]);
            int b = int.Parse(input[1]);
            ListAADigits(a, b);
        }


        static void Main(string[] args)
        {
            Solution();
        }
    }
}
