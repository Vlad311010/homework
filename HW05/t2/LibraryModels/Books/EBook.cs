namespace t2.LibraryModels.Books
{
    internal class EBook : Book
    {
        public string InternetSource { get; private set; }
        public IReadOnlyCollection<string> AvailableFormats => _formats;
        public Task<int?> Pages
        {
            get
            {
                if (_pagesCount == null)
                    return InitPagesCountAsync();

                return Task.FromResult(_pagesCount);
            }
        }

        private string[] _formats;
        private int? _pagesCount = null;


        public EBook(string title, string source, IEnumerable<string> formats, IEnumerable<Author> authors) : base(title, authors)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(source, nameof(source));
            ArgumentNullException.ThrowIfNull(formats, nameof(formats));

            InternetSource = source;
            _formats = formats.ToArray();
        }

        private async Task<int?> InitPagesCountAsync()
        {
            _pagesCount = await BookExternalInfoAccesser.FetchPagesCountData(InternetSource);
            return _pagesCount;
        }
    }
}
