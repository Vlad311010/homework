using Csv;
using t2.LibraryModels.Books;

namespace t2.LibraryModels.CsvParser
{
    internal interface ICsvParsingStrategy
    {
        public bool TryParseCsvRow(ICsvLine line, int lineNumber, out Book? book, out UnparsedRowInfo? unparsedRowInfo);
    }
}
