namespace t2.Library
{
    public class Book
    {
        public string Title { get; private set; }
        public IReadOnlyCollection<Author> Authors => _authors;

        private HashSet<Author> _authors = new HashSet<Author>();

        public Book(string title, IEnumerable<Author> authors)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(title, nameof(title));

            Title = title;

            if (authors == null)
                return;

            foreach (var author in authors)
            {
                _authors.Add(author);
            }
        }

        public override bool Equals(object? obj)
        {
            return obj is Book other
                && Title == other.Title                
                && _authors.SetEquals(other._authors);
        }

        public override int GetHashCode()
        {
            int hash = Title.GetHashCode();

            foreach (var author in _authors)
            {
                hash = HashCode.Combine(hash, author.GetHashCode());
            }
            return hash;
        }

        public override string ToString()
        {
            return $"{Title} - {string.Join(", ", Authors.Select(a => a.FullName))}";
        }
    }
}
