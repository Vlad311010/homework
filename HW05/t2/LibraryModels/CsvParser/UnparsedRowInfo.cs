namespace t2.LibraryModels.CsvParser
{
    internal class UnparsedRowInfo
    {
        public int RowNumber { get; private set; }
        public string? ErrorMessage { get; private set; }
        public string RowContent { get; private set; }

        public UnparsedRowInfo(int rowNumber, string? errorMessage, string content)
        {
            if (rowNumber < 1)
                throw new ArgumentException("Row index cant be negative or equal to zero");
            ArgumentNullException.ThrowIfNull(content, nameof(content));

            RowNumber = rowNumber;
            ErrorMessage = errorMessage;
            RowContent = content;
        }

        public string[] AsCsvRow()
        {
            return new string[] { RowNumber.ToString(), ErrorMessage ?? "", RowContent };
        }
    }
}
