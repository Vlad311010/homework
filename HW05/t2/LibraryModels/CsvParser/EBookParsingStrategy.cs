using Csv;
using t2.LibraryModels.Books;

namespace t2.LibraryModels.CsvParser
{
    internal class EBookParsingStrategy : ICsvParsingStrategy
    {
        private const string AuthorColumnName = "creator";
        private const string TitleColumnName = "title";
        private const string FormatColumnName = "format";
        private const string InternetSourceColumnName = "identifier";

        public bool TryParseCsvRow(ICsvLine line, int lineNumber, out Book? book, out UnparsedRowInfo? unparsedRowInfo)
        {
            bool parsingError = false;
            List<string> errors = new List<string>();
            Author[] authors = BookCsvParser.ParseAuthors(line[AuthorColumnName]);
            string[] formats = line[FormatColumnName].Split(',');
            string source = line[InternetSourceColumnName];
            string title = line[TitleColumnName];


            if (authors.Length == 0)
            {
                errors.Add($"Can't parse {AuthorColumnName}");
                parsingError = true;
            }

            if (string.IsNullOrEmpty(source))
            {
                errors.Add("Missing internet source identifier");
                parsingError = true;
            }

            if (string.IsNullOrEmpty(title))
            {
                errors.Add("Missing book title");
                parsingError = true;
            }

            if (formats == null)
            {
                errors.Add("Can't parse book formats");
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
                book = new EBook(title, source, formats!, authors);
                unparsedRowInfo = null;
                return true;
            }
        }
    }
}
