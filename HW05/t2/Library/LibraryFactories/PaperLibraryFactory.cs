using Csv;
using System.Text.RegularExpressions;
using t2.Library.Books;

namespace t2.Library.LibraryFactory
{
    internal class PaperLibraryFactory : LibraryAbstractFactory
    {
        // csv confing
        private const string AuthorColumnName = "creator";
        private const string PublicationDateColumnName = "publicdate";
        private const string PublisherColumnName = "publisher";
        private const string IdentifiersColumnName = "related-external-id";
        private const string TitleColumnName = "title";

        protected override Catalog ReadSource()
        {
            Catalog catalog = new Catalog();
            List<(int Line, string ErrorInfo, string Content)> unparsedDataInfo = new();
            using (StreamReader reader = new StreamReader(DefaultCsvSource))
            {
                int lineNumber = 1; // CsvReader.Read skips header so I start indexing from second line of csv
                IEnumerable<ICsvLine> lines = CsvReader.Read(reader, _readerOptions); // using external library https://github.com/stevehansen/csv/ for parsing csv parsing

                foreach (var line in lines)
                {
                    lineNumber++;

                    Author[] authors = ParseAuthors(line[AuthorColumnName]);
                    DateOnly? date = ParsePublicationDate(line[PublicationDateColumnName]);
                    string publisher = line[PublisherColumnName];
                    string[] isbns = ParseIsbns(line[IdentifiersColumnName]);
                    string title = line[TitleColumnName];


                    if (authors.Length == 0)
                    {
                        AddWarning(unparsedDataInfo, lineNumber, line.Raw, $"Can't parse {AuthorColumnName}");
                        continue;
                    }

                    if (isbns.Length == 0)
                    {
                        AddWarning(unparsedDataInfo, lineNumber, line.Raw, "Missing ISBN code");
                        continue;
                    }

                    if (string.IsNullOrEmpty(title))
                    {
                        AddWarning(unparsedDataInfo, lineNumber, line.Raw, "Missing book title");
                        continue;
                    }

                    PaperBook book = new PaperBook(title, date, isbns, publisher, authors);
                    catalog[isbns[0]] = book;
                }

            }

            return catalog;
        }

        private void AddWarning(List<(int Line, string Content, string ErrorInfo)> unparsedDataList, int line, string content, string errorInfo)
        {
            unparsedDataList.Add((line, errorInfo, content));
        }

        private string[] ParseIsbns(string identifiers)
        {
            List<string> isbns = new List<string>();
            string regex = @"^urn:isbn:(\d+)";
            foreach (string identifier in identifiers.Split(","))
            {
                var match = Regex.Match(identifier, regex);
                if (match.Success)
                {
                    isbns.Add(match.Groups[1].Value);
                }
            }

            return isbns.ToArray();
        }

        private DateOnly? ParsePublicationDate(string dateTimeString)
        {
            DateTime dateTime;
            if (DateTime.TryParse(dateTimeString, out dateTime))
                return DateOnly.FromDateTime(dateTime);

            return null;
        }
    }
}
