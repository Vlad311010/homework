using System.Net.Http;
using System.Text.RegularExpressions;

namespace t2.LibraryModels.Books
{
    internal class EBook : Book
    {
        public string InternetSource { get; private set; }
        public IReadOnlyCollection<string> AvailableFormats => _formats;
        public Task<int> Pages { get; init; }

        private string[] _formats;
        private int? _pagesCount = null;


        private static readonly HttpClient _httpClient = new HttpClient();

        public EBook(string title, string source, IEnumerable<string> formats, IEnumerable<Author> authors) : base(title, authors)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(source, nameof(source));
            ArgumentNullException.ThrowIfNull(formats, nameof(formats));

            InternetSource = source;
            _formats = formats.ToArray();
            // Pages = FetchPagesCountData;
        }


        private async Task<int> FetchPagesCountData()
        {
            Console.WriteLine("COUBT");
            if (_pagesCount != null)
                return _pagesCount.Value;

            const string detailsURL = @"https://archive.org/details/{0}";
            string pageContent = await _httpClient.GetStringAsync(string.Format(detailsURL, InternetSource));
            string regex = "itemprop=\"numberOfPages\">\\s*(\\d+)\\s*<\\/span>;"; // decided to use regex instead of  parsisng whole html page
            var match = Regex.Match(pageContent, regex);
            if (!match.Success)
                throw new ArgumentException($"Can't fetch number of pages for {nameof(InternetSource)} {InternetSource}");


            int pages;
            if (int.TryParse(match.Groups[1].Value, out pages))
            {
                _pagesCount = pages;
                return _pagesCount.Value;
            }
            else
                return -1;
        }
    }
}
