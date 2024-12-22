using t2.LibraryModels.Books;

namespace t2.LibraryModels.LibraryFactory
{
    internal abstract class ILibraryFactory
    {
        public abstract Library CreateLibrary();
        public abstract IEnumerable<string> GetPressReliseItems(IEnumerable<Book> books);

    }
}
