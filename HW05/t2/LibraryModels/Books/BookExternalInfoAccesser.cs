using System.Text.RegularExpressions;

namespace t2.LibraryModels.Books
{
    internal static class BookExternalInfoAccesser
    {
        private const string InternetSourceTemplate = @"https://archive.org/details/{0}";
        private static readonly HttpClient _httpClient = new HttpClient();

        public static async Task<int?> FetchPagesCountData(string bookExternalIdentifier)
        {
            string pageContent = await _httpClient.GetStringAsync(string.Format(InternetSourceTemplate, bookExternalIdentifier));
            string regex = "itemprop=\"numberOfPages\">\\s*(\\d+)\\s*<\\/span>"; // decided to use regex instead of  parsisng whole html page
            var match = Regex.Match(pageContent, regex);
            if (match.Success && int.TryParse(match.Groups[1].Value, out int pages))
                return pages;
                
            return null;
        }
    }
}
