using Csv;
using System.Text.RegularExpressions;

namespace t2.Library.LibraryFactory
{
    internal abstract class LibraryAbstractFactory
    {
        protected const string DefaultCsvSource = @".\PersistentData\books_info.csv";
        protected readonly CsvOptions _readerOptions = new CsvOptions()
        {
            // csv file contains multiple duplicated empty headers, so I created custom comparer to ignore thouse headers NonEmptyStringComparer
            Comparer = new NonEmptyStringComparer(),
        };

        protected abstract Catalog ReadSource();

        public Library CreateLibrary()
        {
            Catalog catalog = ReadSource();
            return new Library(catalog);
        }

        protected Author[] ParseAuthors(string authors)
        {
            /// ([a-zA-Z]+) first group - last name
            /// [,. ] * coma, period or wihre space
            /// ([a-zA-Z]+) second group - first name
            /// ([,.] *\d+)? fourth group optional - birth year 
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
