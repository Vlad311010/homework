namespace t2.Library
{
    internal class ELibraryFactory : LibraryAbstractFactory
    {
        readonly string defaultCsvSource = @".\PersistentData\book_info.csv";
        public override Library CreateLibrary()
        {
            throw new NotImplementedException();
        }
    }
}
