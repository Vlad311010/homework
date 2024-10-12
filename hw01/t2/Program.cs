namespace t2
{
    internal class Program
    {
        private static string CreateISBN(string digits)
        {
            int checkSum = 0;
            for (int i = 0; i < digits.Length; i++)
            {
                checkSum += (10 - i) * int.Parse(digits[i].ToString());
            }


            int remainder = checkSum % 11;
            int checkDigit = 11 - remainder;
            if (checkDigit == 10)
                return digits + "X";
            else
                return digits + checkDigit;
        }

        private static void Solution()
        {
            string first9Digits = Console.ReadLine();
            Console.WriteLine(CreateISBN(first9Digits));
        }

        static void Main(string[] args)
        {
            Solution();
        }
    }
}
