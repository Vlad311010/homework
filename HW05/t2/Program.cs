using t2.LibraryModels;
using t2.LibraryModels.Books;
using t2.LibraryModels.LibraryFactory;

using t2.Repositories;
using t2.Serializers;

namespace t2
{
    public struct S
    {
        public int x;
        public int y;

        public S()
        {
            x = 3;
        }
    }
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string libraryType = "ELibrary";
            Library eLibrary = CreateLibrarty(libraryType);
            libraryType = "PaperLibrary";
            Library paperLibrary = CreateLibrarty(libraryType);

            await BookExternalInfoAccesser.InitializeBooksPageCount(eLibrary.Catalog);

            IRepository<Catalog> paperRepositoty = new XMLRepository("paper");
            IRepository<Catalog> eRepositoty = new XMLRepository("e");

            paperRepositoty.Serialize(paperLibrary.Catalog);
            eRepositoty.Serialize(eLibrary.Catalog);

            paperRepositoty = new JsonRepository("PaperLibrary");
            eRepositoty = new JsonRepository("ELibrary");
            
            paperRepositoty.Serialize(paperLibrary.Catalog);
            eRepositoty.Serialize(eLibrary.Catalog);
        }


        public static Library CreateLibrarty(string libraryType)
        {
            string csvFile = "./PersistentData/books_info.csv";
            ILibraryFactory libraryFactory = null!;
            switch (libraryType)
            {
                case "ELibrary":
                    libraryFactory = new ELibraryFactory(csvFile);
                    break;
                case "PaperLibrary":
                    libraryFactory = new PaperLibraryFactory(csvFile);
                    break;
                default:
                    throw new ArgumentException($"Unknown library type ({libraryType})");
            }

            return libraryFactory.CreateLibrary();
        }
    }
}
