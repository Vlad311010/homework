using t2.LibraryModels.Books;
using t2.LibraryModels.CsvParser;

namespace t2.LibraryModels.LibraryFactory
{
    internal class PaperLibraryFactory : ILibraryFactory
    {
        private string _dataSourceFile;

        public PaperLibraryFactory(string dataSourceFile)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(dataSourceFile);

            _dataSourceFile = dataSourceFile;
        }

        public override Library CreateLibrary()
        {
            BookCsvParser csvParser = new BookCsvParser(new PaperBookParsingStrategy());
            IEnumerable<Book> books = csvParser.ReadCsv(_dataSourceFile);
            Catalog catalog = new Catalog();
            
            foreach (Book book in books)
            {
                string isbnKey = ((PaperBook)book).ISBNs.First();
                catalog[isbnKey] = book;
            }

            return new Library(catalog, GetPressReliseItems(books));
        }
        
        public override IEnumerable<string> GetPressReliseItems(IEnumerable<Book> books)
        {
            return books.Select(book => ((PaperBook)book).Publisher).Distinct();
        }
    }
}
