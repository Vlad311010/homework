using System.Text.Json;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace t2.Library.SerializationModels
{
    [XmlRoot("Catalog")]
    public class CatalogSerializationModel : IXmlSerializable
    {
        public Dictionary<ISBN13, BookSerializationModel> Entries { get; set; } = new Dictionary<ISBN13, BookSerializationModel>(); 

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

        public XmlSchema? GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
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
            foreach (var keyValue in Entries)
            {
                writer.WriteStartElement(CatalogEntryElementName);
                writer.WriteElementString("ISBN", (string)keyValue.Key);

                writer.WriteStartElement("Book");
                keyValue.Value.WriteXml(writer);
                writer.WriteEndElement();

                writer.WriteEndElement();
            }
        }

        public string ToJson()
        {

            return JsonSerializer.Serialize(this);
        }

        public static CatalogSerializationModel FromJson(string json)
        {
            CatalogSerializationModel? catalogSM = JsonSerializer.Deserialize<CatalogSerializationModel>(json);
            if (catalogSM == null)
                throw new ArgumentException($"Can't parse given json to {typeof(CatalogSerializationModel)} type");

            return catalogSM;
        }
    }
}
