
namespace t2.Library
{
    internal class PaperLibraryFactory : LibraryAbstractFactory
    {
        readonly string defaultCsvSource = @".\PersistentData\books_info.csv";
        // csv confing
        string authorColumnName = "creator";
        string publicationDateColumnName = "publicdate";
        string publisherColumnName = "publisher";
        string isbnsColumnName = "related-external-id";
        string titleColumnName = "identifier";

        public override Library CreateLibrary()
        {
            Catalog catalog = ReadSource();


            return new Library(catalog);
        }

        private Catalog ReadSource()
        {
            Catalog catalog = new Catalog();
            using (StreamReader reader = new StreamReader(defaultCsvSource))
            {
                string[] headers = reader.ReadLine().Split(','); // read headers

                int authorColumnIdx = Array.IndexOf(headers, authorColumnName);
                int publicationDateIdx = Array.IndexOf(headers, publicationDateColumnName);
                int publisherIdx = Array.IndexOf(headers, publisherColumnName);
                int isnbsIdx = Array.IndexOf(headers, isbnsColumnName);
                int titleIdx =  Array.IndexOf(headers, titleColumnName);

                string line;
                while ((line = reader.ReadLine()!) != null)
                {
                    string[] values = line.Split(',');
                    Author[] authors = ParseAuthors(values[authorColumnIdx]);
                    DateOnly date = ParseDate(values[publicationDateIdx]);
                    string publisher = values[publisherIdx];
                    string[] isbns = ParseIsbns(values[isnbsIdx]);
                    string title = values[titleIdx];
                    PaperBook book = new PaperBook(title, date, isbns, publisher, authors);
                    catalog[isbns[0]] = book;
                }
            }
            
            return catalog;
        }

        private string[] ParseIsbns(string isbns)
        {
            return isbns.Split(",");
        }

        private DateOnly ParseDate(string v)
        {
            return DateOnly.FromDateTime(DateTime.Now);
        }

        private Author[] ParseAuthors(string authors)
        {
            return new Author[authors.Length];
        }
    }
}
