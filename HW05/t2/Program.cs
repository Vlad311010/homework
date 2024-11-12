namespace t2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Catalog catalog = new Catalog();
            Book book01 = new Book("B01", DateOnly.FromDateTime(DateTime.Now), new string[]{ "A" });
            Book book02 = new Book("B02", DateOnly.FromDateTime(DateTime.Now), new string[]{ "B", "C" });
            Book book03 = new Book("B03", DateOnly.FromDateTime(DateTime.Now), new string[]{ "C", "A" });
            Book book04 = new Book("B04", DateOnly.FromDateTime(DateTime.Now), new string[]{ "A" });
            catalog.Add("111111111-1--11-1", book01);
            catalog.Add("111111111-1--11-2", book02);
            catalog.Add("111111111-1--11-3", book03);
            catalog.Add("111111111-1--11-4", book04);

            var a = catalog.GetEnumerator();

        
        }
    }
}
