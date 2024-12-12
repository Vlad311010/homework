using t2.LibraryModels.Books;

namespace t2.LibraryModels.LibraryFactory
{
    internal abstract class LibraryAbstractFactory
    {
        public abstract Library CreateLibrary(string dataSourceFile);
        public abstract IEnumerable<string> GetPressReliseItems(IEnumerable<Book> books);

    }
}
