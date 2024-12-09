using Csv;
using System.Text.RegularExpressions;

namespace t2.Library
{
    internal class ELibraryFactory : LibraryAbstractFactory
    {
        // csv confing
        private const string AuthorColumnName = "creator";
        private const string FormatColumnName = "format";
        private const string SourceColumnName = "related-external-id";
        private const string TitleColumnName = "title";

        protected override Catalog ReadSource()
        {

            Catalog catalog = new Catalog();
            using (StreamReader reader = new StreamReader(DefaultCsvSource))
            {
                IEnumerable<ICsvLine> lines = CsvReader.Read(reader, _readerOptions);

                foreach (var line in lines)
                {
                    Author[] authors = ParseAuthors(line[AuthorColumnName]);
                    string[] formats = line[FormatColumnName].Split();
                    string[] sources = ParseOclc(line[SourceColumnName]);
                    string title = line[TitleColumnName];


                    EBook book = new EBook(title, sources, formats, authors);
                    catalog[sources[0]] = book;
                }
            }

            return catalog;
        }

        private string[] ParseOclc(string identifiers)
        {
            List<string> isbns = new List<string>();
            string regex = @"^urn:oclc:(\d+)";
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
    }
}
