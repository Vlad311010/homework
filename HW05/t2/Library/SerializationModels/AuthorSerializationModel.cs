using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace t2.Library.SerializationModels
{
    [XmlRoot("Author")]
    public class AuthorSerializationModel : IXmlSerializable
    {
        public int Id { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly BirthDate { get; private set; }

        public AuthorSerializationModel() { }
        public AuthorSerializationModel(Author author) 
        { 
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

            FirstName = reader.ReadElementContentAsString();
            LastName = reader.ReadElementContentAsString();
            var dateValue = reader.ReadElementContentAsString();
            BirthDate = DateOnly.ParseExact(dateValue, "yyyy.MM.dd");

            reader.ReadEndElement();

            /*bool noNodes = false;
            reader.ReadStartElement("Author");
            while (!noNodes) // read xml while there is any nodes
            {
                while (reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.Name == nameof(FirstName))
                    {
                        FirstName = reader.ReadElementContentAsString();
                    }
                    else if (reader.Name == nameof(LastName))
                    {
                        LastName = reader.ReadElementContentAsString();
                    }   
                    else if (reader.Name == nameof(BirthDate))
                    {
                        var dateValue = reader.ReadElementContentAsString();
                        BirthDate = DateOnly.ParseExact(dateValue, "yyyy.MM.dd");
                    }
                    else
                    {
                        noNodes = reader.Read(); // skip to next xml node
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
            writer.WriteElementString(nameof(FirstName), FirstName);
            writer.WriteElementString(nameof(LastName), LastName);
            writer.WriteElementString(nameof(BirthDate), BirthDate.ToString("yyyy.MM.dd"));
        }
    }
}
