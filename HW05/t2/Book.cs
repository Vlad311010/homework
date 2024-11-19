namespace t2
{
    public class Book
    {
        public string Title { get; private set; }
        public DateOnly? PublicationData { get; private set; }
        public HashSet<string> Authors { get; private set; } = new HashSet<string>();

        public Book(string title, DateOnly? publicationDate, IEnumerable<string>? authors)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException($"{nameof(title)} should be not empty, not null string");

            Title = title;
            PublicationData = publicationDate;

            if (authors == null)
                return;

            foreach (var author in authors)
            {
                if (!string.IsNullOrWhiteSpace(author))
                    Authors.Add(author);
            }
        }

    }
}
