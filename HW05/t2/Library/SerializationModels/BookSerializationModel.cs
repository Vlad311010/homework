using System.Xml.Serialization;
using System.Xml;
using System.Xml.Schema;

namespace t2.Library.SerializationModels
{
    [XmlRoot("Book")]
    public class BookSerializationModel : IXmlSerializable
    {
        public int Id { get; set; }
        public string Title { get; private set; }
        public DateOnly? PublicationData { get; private set; }
        public List<AuthorSerializationModel> Authors { get; private set; } = new List<AuthorSerializationModel>();

        private const string DateStringFormat = "yyyy.MM.dd";
        private const string AuthorsCollectionElementName = "Author";

        public BookSerializationModel() { }

        public BookSerializationModel(Book book) 
        {
            Title = book.Title;
            PublicationData = book.PublicationData;
            Authors = book.Authors.Select(a => new AuthorSerializationModel(a)).ToList();
        }

        public XmlSchema? GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            reader.MoveToContent();
            reader.ReadStartElement();
            {
                Title = reader.ReadElementContentAsString();
                PublicationData = DateOnly.ParseExact(reader.ReadElementContentAsString(), DateStringFormat);
                reader.ReadStartElement("Authors");
                {
                    while (reader.NodeType != XmlNodeType.EndElement)
                    {
                        var author = new AuthorSerializationModel();
                        author.ReadXml(reader);
                        Authors.Add(author);
                    }
                }
                reader.ReadEndElement();
            }
            reader.ReadEndElement();
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteElementString(nameof(Title), Title);
            
            if (PublicationData.HasValue)
                writer.WriteElementString(nameof(PublicationData), PublicationData.Value.ToString(DateStringFormat));

            writer.WriteStartElement(nameof(Authors));
            foreach (var author in Authors)
            {
                writer.WriteStartElement(AuthorsCollectionElementName);
                author.WriteXml(writer);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();            
        }
    }
}
