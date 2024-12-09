namespace t2.Library
{
    internal class EBook : Book
    {
        public IReadOnlyCollection<string> BookSource => _sources;
        public IReadOnlyCollection<string> AvailableFormats => _formats;
        
        string[] _formats;
        string[] _sources;

        public EBook(string title, IEnumerable<string> sources, IEnumerable<string> formats, IEnumerable<Author> authors) : base(title, authors)
        {
            _sources = sources.ToArray();
            _formats = formats.ToArray();
        }
    }
}
