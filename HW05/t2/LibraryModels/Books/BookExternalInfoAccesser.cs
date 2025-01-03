using System.Text.RegularExpressions;

namespace t2.LibraryModels.Books
{
    internal static class BookExternalInfoAccesser
    {
        private const string InternetSourceTemplate = @"https://archive.org/details/{0}";
        private static readonly HttpClient _httpClient = new HttpClient();

        public static async Task<int?> FetchPagesCountData(string bookExternalIdentifier)
        {
            string pageContent;
            try
            {
                pageContent = await _httpClient.GetStringAsync(string.Format(InternetSourceTemplate, bookExternalIdentifier));
            }
            catch (HttpIOException e) 
            { 
                return null;
            }

            string regex = "itemprop=\"numberOfPages\">\\s*(\\d+)\\s*<\\/span>"; // decided to use regex instead of  parsisng whole html page
            var match = Regex.Match(pageContent, regex);
            if (match.Success && int.TryParse(match.Groups[1].Value, out int pages))
                return pages;
                
            return null;
        }

        public static async Task InitializeBooksPageCount(Catalog eBookCatalog)
        {
            IEnumerable<Book> books = eBookCatalog.GetBooks();
            Task[] tasks = new Task[books.Count()];
            int taskIdx = 0;
            foreach (Book book in books)
            {
                EBook? eBook = book as EBook;
                if (eBook == null)
                    throw new ArgumentException("Invalid book type. Pages count can be initialize only for electronic book");

                tasks[taskIdx++] = eBook.Pages;
            }

            await Task.WhenAll(tasks);
        }
    }
}
