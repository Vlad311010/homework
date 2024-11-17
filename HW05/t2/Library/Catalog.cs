namespace t2.Library
{
    public class Catalog
    {
        public int Count => _catalog.Count;
        public Dictionary<ISBN13, Book> Entries => _catalog;

        private Dictionary<ISBN13, Book> _catalog = new Dictionary<ISBN13, Book>();

        public Book this[ISBN13 isbn]
        {
            get
            {
                if (_catalog.TryGetValue(isbn, out Book? book))
                    return book;

                throw new KeyNotFoundException($"Catalog does not containe entry with {isbn} key.");
            }

            set => _catalog[isbn] = value;
        }

        public IEnumerable<Book> WrittenBy(Author author)
        {
            return _catalog
                .Select(keyValue => keyValue.Value)
                .Where(book => book.Authors.Contains(author))
                .OrderBy(book => book.PublicationData);
        }

        public IEnumerable<Book> WrittenBy(string authorFullName)
        {
            return _catalog
                .Select(keyValue => keyValue.Value)
                .Where(book => book.Authors.Any(author => author.FullName == authorFullName))
                .OrderBy(book => book.PublicationData);
        }

        public IEnumerable<string> GetBooks()
        {
            return _catalog
                .Select(keyValue => keyValue.Value.Title)
                .OrderBy(title => title);
        }

        public IEnumerable<(string, int)> GetAuthorBookCounts()
        {
            return _catalog
                .SelectMany(keyValue => keyValue.Value.Authors) // get collection of all authors
                .GroupBy(author => author)
                .Select(group => (group.Key.FullName, group.Count()));
        }
    }
}
