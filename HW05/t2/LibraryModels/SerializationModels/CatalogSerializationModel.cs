using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using t2.LibraryModels.Books;

namespace t2.LibraryModels.SerializationModels
{
    [XmlRoot("Catalog")]
    public class CatalogSerializationModel : IXmlSerializable
    {
        public Dictionary<string, BookSerializationModel> Entries { get; set; } = new Dictionary<string, BookSerializationModel>(); 

        private const string CatalogEntryElementName = "Entry";
        public CatalogSerializationModel() { }

        public CatalogSerializationModel(Catalog catalog)
        {
            if (catalog == null)
                throw new ArgumentNullException(nameof(catalog), "Catalog cannot be null.");

            Entries = catalog.Entries.ToDictionary(
                keyValue => keyValue.Key,
                KeyValue => new BookSerializationModel(KeyValue.Value)
            );
        }

        public Catalog AsCatalog()
        {
            Catalog catalog = new Catalog();
            foreach (var entry in Entries)
            {
                catalog[entry.Key] = new Book(
                        entry.Value.Title, 
                        entry.Value.Authors.Select(authorSM => authorSM.AsAuthor())
                    );
            }
            
            return catalog;
        }


        public XmlSchema? GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));

            reader.MoveToContent();
            reader.ReadStartElement("Catalog");
            {
                while (reader.NodeType != XmlNodeType.EndElement && reader.Name == CatalogEntryElementName)
                {
                    reader.ReadStartElement(CatalogEntryElementName);
                    {
                        string isbn = reader.ReadElementContentAsString();
                        BookSerializationModel book = new BookSerializationModel();
                        book.ReadXml(reader);

                        Entries.Add(isbn, book);
                    }
                    reader.ReadEndElement();
                }
            }
            reader.ReadEndElement();
        }

        public void WriteXml(XmlWriter writer)
        {
            if (writer == null)
                throw new ArgumentNullException(nameof(writer));

            foreach (var keyValue in Entries)
            {
                writer.WriteStartElement(CatalogEntryElementName);
                writer.WriteElementString("Identifier", (string)keyValue.Key);

                writer.WriteStartElement("Book");
                keyValue.Value.WriteXml(writer);
                writer.WriteEndElement();

                writer.WriteEndElement();
            }
        }
    }
}
