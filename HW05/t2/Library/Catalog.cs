using t2.Library.SerializationModels;

namespace t2.Library
{
    public class Catalog
    {
        public int Count => _catalog.Count;
        public IReadOnlyDictionary<ISBN13, Book> Entries => _catalog;

        public Dictionary<ISBN13, Book> _catalog = new Dictionary<ISBN13, Book>();

        public Catalog() { }
        public Catalog(CatalogSerializationModel catalogSM)
        {
            if (catalogSM == null)
                throw new ArgumentNullException(nameof(catalogSM));

            foreach (var entry in catalogSM.Entries)
            {
                this[entry.Key] = new Book(entry.Value);
            }
        }

        public Book this[ISBN13 isbn]
        {
            get
            {
                if (Entries.TryGetValue(isbn, out Book? book))
                    return book;

                throw new KeyNotFoundException($"Catalog does not containe entry with {isbn} key.");
            }

            set => _catalog[isbn] = value;
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

            foreach (ISBN13 isbn in Entries.Keys)
            {
                if (!this[isbn].Equals(other[isbn]))
                {
                    return false;
                }
            }
            
            return true;
        }
        
        /*public override int GetHashCode()
        {
            // Catalog has only one (mutable) field so i think it makes no sence
            // to override GetHashCode for Catalog.
            return base.GetHashCode();
        }*/
    }
}
