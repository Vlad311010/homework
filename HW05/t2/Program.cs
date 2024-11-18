using System.Text.Json;
using System.Xml.Serialization;
using t2.Library;
using t2.Library.SerializationModels;
using t2.Repositories;
using t2.Serializers;

namespace t2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Catalog catalog = new Catalog();

            // authors
            var author01 = new Author("Mark", "Lawrence", new DateOnly(1988, 1, 1));
            var author02 = new Author("Arkady", "Strugatsky", new DateOnly(1925, 8, 28));
            var author03 = new Author("Boris", "Strugatsky", new DateOnly(1933, 4, 14));
            var author04 = new Author("Nisio", "Isin", new DateOnly(1981, 3, 24));
            var author05 = new Author("Neil", "Gaiman", new DateOnly(1960, 11, 10));
            var author06 = new Author("Terry", "Pratchett", new DateOnly(1948, 6, 28));

            // books
            var book01 = new Book("Prince of Thorns", new DateOnly(2012, 7, 31), new Author[] { author01 });
            var book02 = new Book("Roadside Picnic", new DateOnly(1977, 1, 1), new Author[] { author02, author03 });
            var book03 = new Book("BAKEMONOGATARI, Part 1", new DateOnly(2016, 12, 20), new Author[] { author04 });
            var book04 = new Book(
                "Good Omens: The Nice and Accurate Prophecies of Agnes Nutter, Witch",
                new DateOnly(1990, 1, 1), new Author[] { author05, author06 }
            );

            catalog["9781937007683"] = book01;
            catalog["9780026151702"] = book02;
            catalog["9781942993889"] = book03;
            catalog["9780575048003"] = book04;

            /*
            var bookSM = new BookSerializationModel(book02);
            var authorSM = new AuthorSerializationModel(author02);
            var catalogSM = new CatalogSerializationModel(catalog);

            var bookSerializer = new XmlSerializer(typeof(BookSerializationModel));
            var authorSerializer = new XmlSerializer(typeof(AuthorSerializationModel));
            var catalogSerializer = new XmlSerializer(typeof(CatalogSerializationModel));
            */

            XMLRepositoty xmlRepositoty = new XMLRepositoty();
            xmlRepositoty.Serialize(catalog, @"D:\CoherentHomework\homework\HW05\t2\PersistentData\catalog.xml");
            Catalog desXmlCatalog = xmlRepositoty.Deserialize(@"D:\CoherentHomework\homework\HW05\t2\PersistentData\catalog.xml");
            Console.WriteLine("Are equal: " + desXmlCatalog.Equals(catalog));

            JsonRepository jsonRepositoty = new JsonRepository();
            jsonRepositoty.Serialize(catalog, @"D:\CoherentHomework\homework\HW05\t2\PersistentData\catalog.json");
            Catalog desJsonCatalog = jsonRepositoty.Deserialize(@"D:\CoherentHomework\homework\HW05\t2\PersistentData\catalog.json");
            Console.WriteLine("Are equal: " + desJsonCatalog.Equals(catalog));
        }
    }
}
