using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace t2.Library.SerializationModels
{
    [XmlRoot("Author")]
    public class AuthorSerializationModel : IXmlSerializable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly BirthDate { get; set; }

        public AuthorSerializationModel() { }
        public AuthorSerializationModel(Author author) 
        {
            if (author == null)
                throw new ArgumentNullException(nameof(author), "Author cannot be null.");

            FirstName = author.FirstName;
            LastName = author.LastName;
            BirthDate = author.BirthDate;
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

                FirstName = reader.ReadElementContentAsString();
                LastName = reader.ReadElementContentAsString();
                var dateValue = reader.ReadElementContentAsString();
                BirthDate = DateOnly.ParseExact(dateValue, "yyyy.MM.dd");
            }
            reader.ReadEndElement();
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteElementString(nameof(FirstName), FirstName);
            writer.WriteElementString(nameof(LastName), LastName);
            writer.WriteElementString(nameof(BirthDate), BirthDate.ToString("yyyy.MM.dd"));
        }
    }
}
