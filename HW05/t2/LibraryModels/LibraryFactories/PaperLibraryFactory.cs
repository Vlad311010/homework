using t2.LibraryModels.Books;
using t2.LibraryModels.CsvParser;

namespace t2.LibraryModels.LibraryFactory
{
    internal class PaperLibraryFactory : LibraryAbstractFactory
    {
        public override Library CreateLibrary(string dataSourceFile)
        {
            BookCsvParser csvParser = new BookCsvParser(new PaperBookParsingStrategy());
            IEnumerable<Book> books = csvParser.ReadCsv(dataSourceFile);
            Catalog catalog = new Catalog();
            
            foreach (Book book in books)
            {
                string isbnKey = ((PaperBook)book).ISBNs.First();
                catalog[isbnKey] = book;
            }

            return new Library(catalog);
        }

    }
}
