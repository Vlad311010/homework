using t2.Library.Books;

namespace t2.Library
{
    public class Catalog
    {
        public int Count => _catalog.Count;
        public IReadOnlyDictionary<string, Book> Entries => _catalog;

        public Dictionary<string, Book> _catalog = new Dictionary<string, Book>();

        public Book this[string isbn]
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
                .Where(book => book.Authors.Any(a => a.Equals(author)));
        }

        public IEnumerable<(Book, string)> WrittenByWithISBN(Author author)
        {
            return Entries
                .Select(keyValue => (keyValue.Value, keyValue.Key))
                .Where(bookISBNPair => bookISBNPair.Value.Authors.Any(a => a.Equals(author)));
        }


        public IEnumerable<string> GetBookTitles()
        {
            return Entries
                .Select(keyValue => keyValue.Value.Title)
                .OrderBy(title => title);
        }

        public IEnumerable<Book> GetBooks()
        {
            return Entries.Values;
        }

        public IEnumerable<(string, int)> GetAuthorBookCounts()
        {
            return Entries
                .SelectMany(keyValue => keyValue.Value.Authors) // get collection of all authors
                .GroupBy(author => author)
                .Select(group => (group.Key.FullName, group.Count()));
        }

        public IEnumerable<Author> GetAuthors()
        {
            return Entries
                .SelectMany(keyValue => keyValue.Value.Authors);
        }

        
        public override bool Equals(object? obj)
        {
            Catalog? other = obj as Catalog;
            if (other == null) 
                return false;

            foreach (string isbn in Entries.Keys)
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
