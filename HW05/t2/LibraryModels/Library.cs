using t2.LibraryModels.Books;

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

        public async Task InitializeBooksPageCount()
        {
            foreach (Book book in Catalog.GetBooks())
            {
                EBook? eBook = book as EBook;
                if (eBook == null)
                    throw new ArgumentException("Invalid book type. Pages count can be initialize only for electronic book");

                await eBook.Pages;
            }
        }
    }
}
