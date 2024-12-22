using Csv;
using System.Text.RegularExpressions;

namespace t2.LibraryModels.CsvParser
{
    internal abstract class BookParsingBaseStrategy
    {
        protected const string AuthorColumnName = "creator";
        protected const string PublicationDateColumnName = "publicdate";
        protected const string PublisherColumnName = "publisher";
        protected const string TitleColumnName = "title";

        protected static Author[] ParseAuthors(string authors)
        {
            /// ([a-zA-Z]+) first group - last name
            /// ([a-zA-Z]+) second group - first name
            /// (*\d+)? fifth group optional - birth year 
            /// 
            string regex = @"([a-zA-Z.]+)[,.] *([a-zA-Z.]+)(, Sir)?([,.] *(\d+))?";
            string alternativeRegex = @"([a-zA-Z]+) ([a-zA-Z]+)"; // alternative regex for parsing entiries like "Laura Godwin"
            List<Author> authorList = new List<Author>();

            var matches = Regex.Matches(authors, regex);
            if (matches.Count == 0)
                matches = Regex.Matches(authors, alternativeRegex);

            foreach (Match match in matches)
            {
                string lastName = match.Groups[1].Value;
                string firstName = match.Groups[2].Value;

                DateOnly? birthDate = null;
                if (match.Groups[5].Success)
                {
                    int year;
                    if (!int.TryParse(match.Groups[5].Value, out year))
                        throw new FormatException("Can't parse author's birth year");

                    birthDate = new DateOnly(year, 1, 1);
                }

                authorList.Add(new Author(firstName, lastName, birthDate));
            }

            return authorList.ToArray();
        }
    }
}
