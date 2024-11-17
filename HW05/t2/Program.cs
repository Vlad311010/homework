using System.Xml.Serialization;
using t2.Library;
using t2.Library.SerializationModels;

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

            var bookSM = new BookSerializationModel(book02);
            var authorSM = new AuthorSerializationModel(author02);
            var catalogSM = new CatalogSerializationModel(catalog);


            var bookSerializer = new XmlSerializer(typeof(BookSerializationModel));
            var authorSerializer = new XmlSerializer(typeof(AuthorSerializationModel));
            var catalogSerializer = new XmlSerializer(typeof(CatalogSerializationModel));

            string catalogXml;
            using (var writer = new StringWriter())
            {
                catalogSerializer.Serialize(writer, catalogSM);
                catalogXml = writer.ToString();
                Console.WriteLine(catalogXml);
                CatalogSerializationModel csm = catalogSerializer.Deserialize(new StringReader(catalogXml)) as CatalogSerializationModel;

            }

            Console.WriteLine(catalog.Equals(catalog));

            /*string xml;
            using (var writer = new StringWriter())
            {
                bookSerializer.Serialize(writer, bookSM);
                xml = writer.ToString();
                Console.WriteLine(writer.ToString());
                var bsm = bookSerializer.Deserialize(new StringReader(xml)) as BookSerializationModel;
            }*/

                /*using (var writer = new StringWriter())
                {
                    authorSerializer.Serialize(writer,  authorSM);
                    string content = writer.ToString();
                    Console.WriteLine(content);
                    var asm = authorSerializer.Deserialize(new StringReader(content)) as AuthorSerializationModel;
                }*/


                /*BookSerializationModel bsm = bookSerializer.Deserialize(new StringReader(xml)) as BookSerializationModel;
                Console.WriteLine(bsm.Authors.Count);
                Console.WriteLine(bsm.Title);
                Console.WriteLine(bsm.PublicationData);*/



                /*using (var writer = new StringWriter())
                {
                    authorSerializer.Serialize(writer, authorSM);
                    Console.WriteLine(writer.ToString());
                }*/

                /*XmlSerializer nodeSerializer = new XmlSerializer(typeof(XmlNode));
                XmlNode myNode = new XmlDocument().
                CreateNode(XmlNodeType.Element, "AuthorNode", null);
                using (var writer = new StringWriter())
                {
                    authorSerializer.Serialize(writer, authorSM);
                    myNode.InnerText = writer.ToString();
                }*/
            }
        }
}
