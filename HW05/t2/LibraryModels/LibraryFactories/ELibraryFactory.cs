using t2.LibraryModels.Books;
using t2.LibraryModels.CsvParser;

namespace t2.LibraryModels.LibraryFactory
{
    internal class ELibraryFactory : LibraryAbstractFactory
    {
        public override Library CreateLibrary(string dataSourceFile)
        {
            BookCsvParser csvParser = new BookCsvParser(new EBookParsingStrategy());
            IEnumerable<Book> books = csvParser.ReadCsv(dataSourceFile);
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
