namespace t2
{
    public class Catalog
    {
        public int Count => _catalog.Count;
        
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
                .Select(keyValue => keyValue.Value.Title)
                .OrderBy(title => title);
        }

        public IEnumerable<(string, int)> GetAuthorBookCounts()
        {
            return _catalog
                .SelectMany(keyValue => keyValue.Value.Authors) // get collection of all authors
                .GroupBy(author => author)
                .Select(group => (group.Key, group.Count()));
        }
    }
}
