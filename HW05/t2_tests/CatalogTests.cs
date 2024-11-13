using t2;

namespace t2_tests
{
    [TestClass]
    public class CatalogTests
    {
        Catalog catalog;
        Book book00;
        Book book01;
        Book book02;
        Book book03;

        [TestInitialize]
        public void Init()
        {
            catalog = new Catalog();

            book01 = new Book("Title01", new DateOnly(2024, 11, 13), new string[] { "Author01", "Author02" });
            book02 = new Book("Title02", new DateOnly(2010, 8, 13), new string[] { "Author03" });
            book03 = new Book("Title03", new DateOnly(2012, 7, 27), new string[] { "Author02" });
            catalog["2024111300000"] = book01;
            catalog["2010813000000"] = book02;
            catalog["2010727000000"] = book03;

            book00 = new Book("Title00", new DateOnly(2024, 11, 14), new string[] { "Author01", "Author02" });
        }

        [TestMethod]
        public void ISNBSimpleFormInsert()
        {
            string isbn = "111-1-11-111111-1";
            string isbnSimple = "1111111111111";
            catalog[isbnSimple] = book00;

            Assert.ReferenceEquals(catalog[isbn], catalog[isbnSimple]);
        }

        [TestMethod]
        public void ISNBInsert()
        {
            string isbn = "123-4-56-111111-7";
            string isbnSimple = "1234561111117";
            catalog[isbn] = book00;

            Assert.ReferenceEquals(catalog[isbn], catalog[isbnSimple]);
        }

        [TestMethod]
        [DataRow("123-4-56-111111-11", DisplayName = "Additional digit")]
        [DataRow("123-4-56-11111-11", DisplayName = "Misplaced dash")]
        [DataRow("123-4-56-111-111-1", DisplayName = "Additional dash")]
        public void ISBNInvalidFormat(string isbn)
        {
            Assert.ThrowsException<ArgumentException>(() => catalog[isbn]);
        }
        
        [TestMethod]
        public void GetBooksCount()
        {
            Assert.AreEqual(catalog.GetBooks().Count(), 3);
        }

        [TestMethod]
        public void GetBooks()
        {
            string[] expectedBooks = new string[3] { book01.Title, book02.Title, book03.Title};
            string[] books = catalog.GetBooks().ToArray();
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
            (string,int)[] books = catalog.GetAuthorBookCounts().ToArray();
            CollectionAssert.AreEqual(books, expectedBooks);
        }

        [TestMethod]
        public void WrittenByAuthor02()
        {
            string author = "Author02";
            var expectedBooks = new Book[] { book01, book03 };
            Book[] books = catalog.WrittenBy(author).ToArray();
            CollectionAssert.AreEquivalent(books, expectedBooks);
        }

        [TestMethod]
        public void WrittenByAuthor01()
        {
            string author = "Author01";
            var expectedBooks = new Book[] { book01 };
            Book[] books = catalog.WrittenBy(author).ToArray();
            CollectionAssert.AreEquivalent(books, expectedBooks);
        }
    }
}