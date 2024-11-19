using t2;

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

        [TestInitialize]
        public void Init()
        {
            _catalog = new Catalog();

            _book01 = new Book("Title01", new DateOnly(2024, 11, 13), new string[] { "Author01", "Author02" });
            _book02 = new Book("Title02", new DateOnly(2010, 8, 13), new string[] { "Author03" });
            _book03 = new Book("Title03", new DateOnly(2012, 7, 27), new string[] { "Author02" });
            _catalog["2024111300000"] = _book01;
            _catalog["2010813000000"] = _book02;
            _catalog["2010727000000"] = _book03;

            _book00 = new Book("Title00", new DateOnly(2024, 11, 14), new string[] { "Author01", "Author02" });
        }

        [TestMethod]
        public void ISNBSimpleFormInsert()
        {
            string isbn = "111-1-11-111111-1";
            string isbnSimple = "1111111111111";
            _catalog[isbnSimple] = _book00;

            Assert.ReferenceEquals(_catalog[isbn], _catalog[isbnSimple]);
        }

        [TestMethod]
        public void ISNBInsert()
        {
            string isbn = "123-4-56-111111-7";
            string isbnSimple = "1234561111117";
            _catalog[isbn] = _book00;

            Assert.ReferenceEquals(_catalog[isbn], _catalog[isbnSimple]);
        }

        [TestMethod]
        [DataRow("123-4-56-111111-11", DisplayName = "Additional digit")]
        [DataRow("123-4-56-11111-11", DisplayName = "Misplaced dash")]
        [DataRow("123-4-56-111-111-1", DisplayName = "Additional dash")]
        public void ISBNInvalidFormat(string isbn)
        {
            Assert.ThrowsException<ArgumentException>(() => _catalog[isbn]);
        }
        
        [TestMethod]
        public void GetBooksCount()
        {
            Assert.AreEqual(_catalog.GetBooks().Count(), 3);
        }

        [TestMethod]
        public void GetBooks()
        {
            string[] expectedBooks = new string[3] { _book01.Title, _book02.Title, _book03.Title};
            string[] books = _catalog.GetBooks().ToArray();
            CollectionAssert.AreEqual(books, expectedBooks);
        }


        [TestMethod]
        public void GetAuthorBooksCount()
        {
            (string, int)[] expectedBooks = new (string, int)[3] 
            {
                ("Author01", 1),
                ("Author02", 2),
                ("Author03", 1),
            };
            (string,int)[] books = _catalog.GetAuthorBookCounts().ToArray();
            CollectionAssert.AreEqual(books, expectedBooks);
        }

        [TestMethod]
        public void WrittenByAuthor02()
        {
            string author = "Author02";
            var expectedBooks = new Book[] { _book01, _book03 };
            Book[] books = _catalog.WrittenBy(author).ToArray();
            CollectionAssert.AreEquivalent(books, expectedBooks);
        }

        [TestMethod]
        public void WrittenByAuthor01()
        {
            string author = "Author01";
            var expectedBooks = new Book[] { _book01 };
            Book[] books = _catalog.WrittenBy(author).ToArray();
            CollectionAssert.AreEquivalent(books, expectedBooks);
        }
    }
}