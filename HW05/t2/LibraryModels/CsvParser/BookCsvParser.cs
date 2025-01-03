﻿using Csv;
using t2.LibraryModels.Books;

namespace t2.LibraryModels.CsvParser
{
    internal class BookCsvParser
    {
        public BookCsvParser(ICsvParsingStrategy parsingStrategy)
        {
            _strategy = parsingStrategy;
        }

        private ICsvParsingStrategy _strategy;

        private static readonly CsvOptions _readerOptions = new CsvOptions()
        {
            // csv file contains multiple duplicated empty headers, so I created custom comparer to ignore thouse headers NonEmptyStringComparer
            Comparer = new NonEmptyStringComparer(),
        };

        private static readonly string _logFile = "./PersistentData/parsing_error_log_{0}.csv";
        private static readonly string[] _logFileHeaders = new string[] { "RowNumber", "ErrorInfo", "RowContent" };

        public IEnumerable<Book> ReadCsv(string csvFile)
        {
            using (StreamReader reader = new StreamReader(csvFile))
            {
                List<Book> books = new List<Book>();
                Catalog catalog = new Catalog();
                int lineNumber = 2; // CsvReader.Read skips header so I start indexing from second line of csv
                IEnumerable<ICsvLine> lines = CsvReader.Read(reader, _readerOptions); // using external library https://github.com/stevehansen/csv/ for csv parsing
                List<UnparsedRowInfo> unparsedDataInfo = new();
                foreach (var line in lines)
                {
                    UnparsedRowInfo? unparsedRowInfo;
                    if (_strategy.TryParseCsvRow(line, lineNumber, out Book? book, out unparsedRowInfo))
                        books.Add(book!);
                    else
                        unparsedDataInfo.Add(unparsedRowInfo!);
                    
                    lineNumber++;
                }

                WriteParsingErrorLog(unparsedDataInfo);
                return books;
            }
        }

        public void WriteParsingErrorLog(List<UnparsedRowInfo> unparsedRows)
        {
            if (unparsedRows == null || unparsedRows.Count() == 0)
                return;

            string fileName = string.Format(_logFile, _strategy.GetType().Name);
            using (FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (TextWriter writer = new StreamWriter(fileStream))
                {
                    CsvWriter.Write(writer, _logFileHeaders, unparsedRows.Select(data => data.AsCsvRow()));
                }
            }
        }

    }
}
