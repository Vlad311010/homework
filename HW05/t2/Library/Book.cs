namespace t2.Library
{
    public class Book
    {
        public string Title { get; private set; }
        public DateOnly? PublicationData { get; private set; }
        public HashSet<Author> Authors { get; private set; } = new HashSet<Author>();

        public Book(string title, DateOnly? publicationDate, IEnumerable<Author> authors)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(title, nameof(title));

            Title = title;
            PublicationData = publicationDate;

            foreach (var author in authors)
            {
                Authors.Add(author);
            }
        }
    }
}
