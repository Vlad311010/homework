using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace t2.Library.SerializationModels
{
    [XmlRoot("Catalog")]
    public class CatalogSerializationModel : IXmlSerializable
    {
        public Dictionary<ISBN13, BookSerializationModel> Entries = new Dictionary<ISBN13, BookSerializationModel>();

        private const string CatalogEntryElementName = "Entry";
        public CatalogSerializationModel() { }

        public CatalogSerializationModel(Catalog catalog)
        {
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

            while (reader.NodeType != XmlNodeType.EndElement && reader.Name == CatalogEntryElementName)
            {
                reader.ReadStartElement(CatalogEntryElementName);
                {
                    string isbn = reader.ReadElementContentAsString();
                    
                    var book = new BookSerializationModel();
                    book.ReadXml(reader);
                    
                    Entries.Add(isbn, book);

                    /*reader.ReadStartElement("Book");
                    while (reader.NodeType != XmlNodeType.EndElement)
                    {
                        var book = new BookSerializationModel();
                        book.ReadXml(reader);
                        Entries.Add(isbn, book);
                    }
                    reader.ReadEndElement();*/
                }
                reader.ReadEndElement();
            }

            reader.ReadEndElement();


            /*bool noNodes = false;
            reader.ReadStartElement("Catalog");
            while (!noNodes)
            {
                while (reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.Name == CatalogEntryElementName)
                    {
                        string isbn = "";
                        BookSerializationModel? book = new BookSerializationModel();

                        reader.ReadStartElement(CatalogEntryElementName);
                        while (reader.NodeType == XmlNodeType.Element)
                        {
                            if (reader.Name == "ISBN")
                            {
                                isbn = reader.ReadElementContentAsString();
                            }
                            if (reader.Name == "Book")
                            {
                                book.ReadXml(reader);
                            }
                        }

                        if (!string.IsNullOrEmpty(isbn) && book != null)
                            Entries.Add(isbn, book);
                        
                        reader.ReadEndElement();
                    }
                    else
                    {
                        if (noNodes)
                            break;
                        noNodes = reader.Read();
                    }
                }
                if (noNodes)
                    break;
                noNodes = reader.Read();
            }
            reader.ReadEndElement();*/
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
    }
}
