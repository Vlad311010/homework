using t2.Library.SerializationModels;

namespace t2.Library
{
    public class Book
    {
        public string Title { get; private set; }
        public DateOnly? PublicationData { get; private set; }
        public IReadOnlyCollection<Author> Authors => _authors;

        private HashSet<Author> _authors = new HashSet<Author>();

        public Book(string title, DateOnly? publicationDate, IEnumerable<Author> authors)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(title, nameof(title));

            Title = title;
            PublicationData = publicationDate;

            foreach (var author in authors)
            {
                _authors.Add(author);
            }
        }

        public Book(BookSerializationModel bookSM) : this(bookSM.Title, bookSM.PublicationData, bookSM.Authors.Select(a => new Author(a))) { }

        public override bool Equals(object? obj)
        {
            return obj is Book other
                && Title == other.Title
                && PublicationData == other.PublicationData
                && _authors.SetEquals(other._authors);
        }

        public override int GetHashCode()
        {
            int hash = Title.GetHashCode();
            hash = HashCode.Combine(hash, PublicationData?.GetHashCode() ?? 0);

            foreach (var author in _authors)
            {
                hash = HashCode.Combine(hash, author.GetHashCode());
            }
            return hash;
        }
    }
}
