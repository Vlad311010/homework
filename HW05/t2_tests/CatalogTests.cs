using t2.LibraryModels;
using t2.LibraryModels.Books;

namespace t2_tests
{
    [TestClass]
    public class CatalogTests
    {
        Catalog _catalog;

        Book _book00;
        Book _book01;
        Book _book02;
        Book _book03;

        Author _author01;
        Author _author02;
        Author _author03;

        [TestInitialize]
        public void Init()
        {
            _catalog = new Catalog();

            _author01 = new Author("Mark", "Lawrence", new DateOnly(1988, 1, 1));
            _author02 = new Author("Arkady", "Strugatsky", new DateOnly(1925, 8, 28));
            _author03 = new Author("Boris", "Strugatsky", new DateOnly(1933, 4, 14));

            _book01 = new Book("Title01", new Author[] { _author01, _author02 });
            _book02 = new Book("Title02", new Author[] { _author03 });
            _book03 = new Book("Title03", new Author[] { _author02 });
          
            _catalog["2024111300000"] = _book01;
            _catalog["2010813000000"] = _book02;
            _catalog["2010727000000"] = _book03;

            _book00 = new Book("Title00", new Author[] { _author01, _author02 });
        }


        
        [TestMethod]
        public void GetBooksCount()
        {
            int expectedBookCount = 3;
            int bookCount = _catalog.GetBookTitles().Count();
            Assert.AreEqual(bookCount, expectedBookCount, $"Expected value: {expectedBookCount}. Actual value: {bookCount}");
        }

        [TestMethod]
        public void GetBooks()
        {
            string[] expectedBooks = new string[3] { _book01.Title, _book02.Title, _book03.Title};
            string[] books = _catalog.GetBookTitles().ToArray();
            CollectionAssert.AreEqual(books, expectedBooks);
        }


        [TestMethod]
        public void GetAuthorBooksCount()
        {
            (string, int)[] expectedBooks = new (string, int)[3] 
            {
                (_author01.FullName, 1),
                (_author02.FullName, 2),
                (_author03.FullName, 1),
            };
            (string,int)[] books = _catalog.GetAuthorBookCounts().ToArray();
            CollectionAssert.AreEqual(books, expectedBooks, $"Books count differs from expected value in {nameof(expectedBooks)}");
        }

        [TestMethod]
        public void WrittenByAuthor02()
        {
            var expectedBooks = new Book[] { _book01, _book03 };
            Book[] books = _catalog.WrittenBy(_author02).ToArray();
            CollectionAssert.AreEquivalent(books, expectedBooks, $"Collections {nameof(expectedBooks)} and {nameof(books)} are not equivalent");
        }

        [TestMethod]
        public void WrittenByAuthor01()
        {
            var expectedBooks = new Book[] { _book01 };
            Book[] books = _catalog.WrittenBy(_author01).ToArray();
            CollectionAssert.AreEquivalent(books, expectedBooks, $"Collections {nameof(expectedBooks)} and {nameof(books)} are not equivalent");
        }
    }
}