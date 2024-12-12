namespace t2.LibraryModels.LibraryFactory
{
    internal abstract class LibraryAbstractFactory
    {
        public abstract Library CreateLibrary(string dataSourceFile);
        
    }
}
