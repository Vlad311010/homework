using System.Text.RegularExpressions;


namespace t2
{
    internal class Catalog
    {
        Dictionary<string, Book> _catalog = new Dictionary<string, Book>();

        public void Add(string isbn, Book book)
        {
            IsCorrectISBN(isbn);
            try
            {
                _catalog.Add(SimplifyISBN(isbn), book);
            }
            catch (ArgumentException e)
            {
                // cant update book 

                throw;
            }
        }

        private bool IsCorrectISBN(string isbn)
        {
            isbn = SimplifyISBN(isbn);
            return Regex.IsMatch(isbn, "^[0-9]{13}$");
        }

        private string SimplifyISBN(string isbn)
        {
            return isbn.Replace("-", "");
        }

        public IEnumerable<Book> WrittenBy(string author)
        {
            // Retrieve from the catalog a set of books by the specified author name. Books should be sorted by publication date
            return _catalog
                .Select(keyValue => keyValue.Value)
                .Where(book => book.Authors.Contains(author))
                .OrderBy(book => book.PublicationData);
        }

        public IEnumerable<string> GetBook()
        {
            // Get a set of book titles from the catalog, sorted alphabetically
            return _catalog
                .Select(x => x.Value.Title)
                .OrderBy(title => title);
        }

        public IEnumerable<(string, int)> GetAuthorBookCounts()
        {
            return _catalog
                .Select(KeyValue => KeyValue.Value.Authors)
                .SelectMany(authors =>
                    authors.Select(author => (author, CountAuthorOccurence(author))))
                .ToHashSet(); // remove duplicates
        }

        private int CountAuthorOccurence(string author)
        {
            return _catalog
                .Select(x => x.Value)
                .Count(book => book.Authors.Contains(author));
        }
    }
}
