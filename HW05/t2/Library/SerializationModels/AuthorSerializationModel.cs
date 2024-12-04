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
        public DateOnly? BirthDate { get; set; }

        private const string DateStringFormat = "yyyy.MM.dd";

        public AuthorSerializationModel() { }
        public AuthorSerializationModel(Author author) 
        {
            if (author == null)
                throw new ArgumentNullException(nameof(author), "Author cannot be null.");

            FirstName = author.FirstName;
            LastName = author.LastName;
            BirthDate = author.BirthDate;
        }

        public Author AsAuthor()
        {
            return new Author(FirstName, LastName, BirthDate);
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

                FirstName = reader.ReadElementContentAsString();
                LastName = reader.ReadElementContentAsString();
                var dateValue = reader.ReadElementContentAsString();
                BirthDate = string.IsNullOrEmpty(dateValue) ? null : DateOnly.ParseExact(dateValue, DateStringFormat);
            }
            reader.ReadEndElement();
        }

        public void WriteXml(XmlWriter writer)
        {
            if (writer == null)
                throw new ArgumentNullException(nameof(writer));

            writer.WriteElementString(nameof(FirstName), FirstName);
            writer.WriteElementString(nameof(LastName), LastName);
            writer.WriteElementString(nameof(BirthDate), BirthDate == null ? "" : BirthDate.Value.ToString(DateStringFormat));
        }
    }
}
