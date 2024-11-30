namespace t2.Library
{
    internal class PaperBook : Book
    {
        public IReadOnlyCollection<ISBN13> ISBNs => _isbns;
        public string Publisher { get; private set; }
        
        private ISBN13[] _isbns;

        public PaperBook(string title, DateOnly? publicationDate, IEnumerable<ISBN13> isbnCodes, string publisher, IEnumerable<Author> authors) : base(title, publicationDate, authors)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(publisher, nameof(publisher));
            if (isbnCodes == null || isbnCodes.Count() == 0)
                throw new ArgumentException("Paper book must have at least one ISBN code");

            _isbns = isbnCodes.ToArray();
        }
    }
}
