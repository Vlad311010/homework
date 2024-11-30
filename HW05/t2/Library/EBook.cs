namespace t2.Library
{
    internal class EBook : Book
    {
        public Uri BookSource { get; private set; }
        public IReadOnlyCollection<EBookFormat> AvailableFormats => _formats;
        public EBookFormat[] _formats { get; private set; }

        public EBook(string title, DateOnly? publicationDate, Uri url, IEnumerable<EBookFormat> formats, IEnumerable<Author> authors) : base(title, publicationDate, authors)
        {
            BookSource = url;
            _formats = formats.ToArray();
        }
    }
}
