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
                if (Entries.TryGetValue(isbn, out Book? book))
                    return book;

                throw new KeyNotFoundException($"Catalog does not containe entry with {isbn} key.");
            }

            set => Entries[isbn] = value;
        }

        public IEnumerable<Book> WrittenBy(Author author)
        {
            return Entries
                .Select(keyValue => keyValue.Value)
                .Where(book => book.Authors.Contains(author))
                .OrderBy(book => book.PublicationData);
        }

        public IEnumerable<Book> WrittenBy(string authorFullName)
        {
            return Entries
                .Select(keyValue => keyValue.Value)
                .Where(book => book.Authors.Any(author => author.FullName == authorFullName))
                .OrderBy(book => book.PublicationData);
        }

        public IEnumerable<string> GetBooks()
        {
            return Entries
                .Select(keyValue => keyValue.Value.Title)
                .OrderBy(title => title);
        }

        public IEnumerable<(string, int)> GetAuthorBookCounts()
        {
            return Entries
                .SelectMany(keyValue => keyValue.Value.Authors) // get collection of all authors
                .GroupBy(author => author)
                .Select(group => (group.Key.FullName, group.Count()));
        }

        public override bool Equals(object? obj)
        {
            Catalog? other = obj as Catalog;
            if (other == null) 
                return false;

            return Entries.SequenceEqual(other.Entries);
        }
        
        /*public override int GetHashCode()
        {
            // Catalog has only one (mutable) field so i think it makes no sence
            // to override GetHashCode for Catalog.
            return base.GetHashCode();
        }*/
    }
}
