namespace t2.LibraryModels.Books
{
    internal class EBook : Book
    {
        public string InternetSource { get; private set; }
        public IReadOnlyCollection<string> AvailableFormats => _formats;

        string[] _formats;

        public EBook(string title, string source, IEnumerable<string> formats, IEnumerable<Author> authors) : base(title, authors)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(source, nameof(source));
            ArgumentNullException.ThrowIfNull(formats, nameof(formats));

            InternetSource = source;
            _formats = formats.ToArray();
        }
    }
}
