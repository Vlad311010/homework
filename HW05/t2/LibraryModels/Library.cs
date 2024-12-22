namespace t2.LibraryModels
{
    internal class Library
    {
        public Catalog Catalog { get; private set; }
        public IReadOnlyCollection<string> PressReleaseItems => _pressReleaseItems;

        private string[] _pressReleaseItems;

        public Library(Catalog catalog, IEnumerable<string> pressReleaseItems)
        {
            ArgumentNullException.ThrowIfNull(catalog, nameof(catalog));
            ArgumentNullException.ThrowIfNull(pressReleaseItems, nameof(pressReleaseItems));

            Catalog = catalog;
            _pressReleaseItems = pressReleaseItems.ToArray();
        }
    }
}
