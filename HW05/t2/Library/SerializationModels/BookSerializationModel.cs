using System.Xml.Serialization;
using System.Xml;
using System.Xml.Schema;
using t2.Library.Books;

namespace t2.Library.SerializationModels
{
    [XmlRoot("Book")]
    public class BookSerializationModel : IXmlSerializable
    {
        public string Title { get; set; }
        public List<AuthorSerializationModel> Authors { get; set; } = new List<AuthorSerializationModel>();

        private const string DateStringFormat = "yyyy.MM.dd";
        private const string AuthorsCollectionElementName = "Author";

        public BookSerializationModel() { }

        public BookSerializationModel(Book book) 
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book), "Book cannot be null.");

            Title = book.Title;
            Authors = book.Authors.Select(a => new AuthorSerializationModel(a)).ToList();
        }

        public Book AsBook()
        {
            return new Book(
                Title, 
                Authors.Select(authorSM => authorSM.AsAuthor())
            );
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
            reader.ReadStartElement();
            {
                Title = reader.ReadElementContentAsString();
                reader.ReadStartElement(nameof(Authors));
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
            if (writer == null)
                throw new ArgumentNullException(nameof(writer));

            writer.WriteElementString(nameof(Title), Title);
            
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
