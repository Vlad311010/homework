namespace t2.LibraryModels
{
    internal class Library
    {
        public Catalog Catalog { get; private set; }
        public IReadOnlyList<string> PressReleaseItems => _pressReleaseItems;

        private List<string> _pressReleaseItems = new List<string>();
        // private Catalog _catalog;

        public Library(Catalog catalog)
        {
            ArgumentNullException.ThrowIfNull(catalog, nameof(catalog));
            
            Catalog = catalog;
            // TODO: set _pressReleaseItems
        }
    }
}
