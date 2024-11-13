namespace t2
{
    public class Catalog
    {
        Dictionary<ISBN13, Book> _catalog = new Dictionary<ISBN13, Book>();

        public ICollection<ISBN13> Keys => _catalog.Keys;

        public ICollection<Book> Values => _catalog.Values;

        public int Count => _catalog.Count;

        public bool IsReadOnly => false;

        public Book this[ISBN13 isbn] 
        {
            get
            {
                return _catalog[isbn];
            }
            set 
            {
                _catalog[isbn] = value;
            }
        }

        public IEnumerable<Book> WrittenBy(string author)
        {
            // Retrieve from the catalog a set of books by the specified author name. Books should be sorted by publication date
            return _catalog
                .Select(keyValue => keyValue.Value)
                .Where(book => book.Authors.Contains(author))
                .OrderBy(book => book.PublicationData);
        }

        public IEnumerable<string> GetBooks()
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
