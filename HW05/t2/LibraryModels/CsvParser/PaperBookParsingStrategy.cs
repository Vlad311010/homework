using Csv;
using System.Text.RegularExpressions;
using t2.LibraryModels.Books;

namespace t2.LibraryModels.CsvParser
{
    internal class PaperBookParsingStrategy : ICsvParsingStrategy
    {
        private const string AuthorColumnName = "creator";
        private const string PublicationDateColumnName = "publicdate";
        private const string PublisherColumnName = "publisher";
        private const string IdentifiersColumnName = "related-external-id";
        private const string TitleColumnName = "title";

        public bool TryParseCsvRow(ICsvLine line, int lineNumber, out Book? book, out UnparsedRowInfo? unparsedRowInfo)
        {
            bool parsingError = false;
            List<string> errors = new List<string>();
            Author[] authors = BookCsvParser.ParseAuthors(line[AuthorColumnName]);
            DateOnly? date = ParsePublicationDate(line[PublicationDateColumnName]);
            string publisher = line[PublisherColumnName];
            string[] isbns = ParseIsbns(line[IdentifiersColumnName]);
            string title = line[TitleColumnName];


            if (authors.Length == 0)
            {
                errors.Add($"Can't parse {AuthorColumnName}");
                parsingError = true;
            }

            if (isbns.Length == 0)
            {
                errors.Add("Missing ISBN code");
                parsingError = true;
            }

            if (string.IsNullOrEmpty(title))
            {
                errors.Add("Missing book title");
                parsingError = true;
            }

            if (parsingError)
            {
                book = null;
                unparsedRowInfo = new UnparsedRowInfo(lineNumber, string.Join(',', errors), line.Raw);
                return false;
            }
            else
            {
                book = new PaperBook(title, date, isbns, publisher, authors);
                unparsedRowInfo = null;
                return true;
            }
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
