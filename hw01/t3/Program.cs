
namespace t3
{
    internal class Program
    {

        private static void PrintArray(int[] arr)
        {
            Console.WriteLine('[' + string.Join(',', arr) + ']');
        }

        private static bool Contains(int[] arr, int value)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == value)
                    return true;
            }

            return false;
        }

        private static int[] UniqueElements(int[] arr)
        {
            int[] uniqueElements = new int[arr.Length];
            uniqueElements[0] = arr[0];
            int uniqueElementsPointer = 1;
            for (int i = 1; i < arr.Length; i++)
            {
                if (!Contains(uniqueElements, arr[i]))
                    uniqueElements[uniqueElementsPointer++] = arr[i];
            }

            Array.Resize(ref uniqueElements, uniqueElementsPointer);
            return uniqueElements;
        }

        private static void Solution()
        {
            Console.WriteLine("Enter array length");
            int arrayLength = int.Parse(Console.ReadLine()!);
            int[] array = new int[arrayLength];
            for (int i = 0; i < arrayLength; i++)
            {
                Console.WriteLine($"Enter array element {i}");
                array[i] = int.Parse(Console.ReadLine()!);
            }

            int[] uniqueElementss = UniqueElements(array);
            PrintArray(uniqueElementss);
        }

        static void Main(string[] args)
        {
            Solution();
        }
    }
}
