namespace t2.Library
{
    internal class PaperBook : Book
    {
        public IReadOnlyCollection<string> ISBNs => _isbns;
        public DateOnly? PublicationData { get; private set; }
        public string Publisher { get; private set; }
        
        private string[] _isbns;

        public PaperBook(string title, DateOnly? publicationDate, IEnumerable<string> isbnCodes, string publisher, IEnumerable<Author> authors) : base(title, authors)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(publisher, nameof(publisher));
            if (isbnCodes == null || isbnCodes.Count() == 0)
                throw new ArgumentException("Paper book must have at least one ISBN code");

            Publisher = publisher;
            PublicationData = publicationDate;
            _isbns = isbnCodes.ToArray();
        }
    }
}
