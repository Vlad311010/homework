using t2.LibraryModels.Books;
using t2.LibraryModels.CsvParser;

namespace t2.LibraryModels.LibraryFactory
{
    internal class ELibraryFactory : ILibraryFactory
    {
        private string _dataSourceFile;

        public ELibraryFactory(string dataSourceFile)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(dataSourceFile);

            _dataSourceFile = dataSourceFile;
        }

        public override Library CreateLibrary()
        {
            BookCsvParser csvParser = new BookCsvParser(new EBookParsingStrategy());
            IEnumerable<Book> books = csvParser.ReadCsv(_dataSourceFile);
            Catalog catalog = new Catalog();

            foreach (Book book in books)
            {
                string sourceKey = ((EBook)book).InternetSource;
                catalog[sourceKey] = book;
            }

            return new Library(catalog, GetPressReliseItems(books));
        }

        public override IEnumerable<string> GetPressReliseItems(IEnumerable<Book> books)
        {
            return books.SelectMany(book => ((EBook)book).AvailableFormats).Distinct();
        }
    }
}
