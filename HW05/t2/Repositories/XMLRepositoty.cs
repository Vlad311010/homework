using System.Xml;
using System.Xml.Serialization;
using t2.Library;
using t2.Library.SerializationModels;
using t2.Repositories;

namespace t2.Serializers
{
    internal class XMLRepositoty : IRepository<Catalog>
    {
        private static readonly XmlSerializer Serializer = new XmlSerializer(typeof(CatalogSerializationModel));

        public void Serialize(Catalog catalog, string filePath)
        {
            CatalogSerializationModel catalogSM = new CatalogSerializationModel(catalog);
            using (var writer = new StreamWriter(filePath))
            {
                Serializer.Serialize(writer, catalogSM);
            }
        }

        public Catalog Deserialize(string filePath) 
        {
            try
            {
                using (var reader = new StreamReader(filePath))
                {
                    CatalogSerializationModel? catalogSM = Serializer.Deserialize(reader) as CatalogSerializationModel;
                    if (catalogSM == null)
                        throw new XmlException("Can't parse given xml file");

                    return new Catalog(catalogSM);
                }
            }
            catch (FileNotFoundException ex)
            {
                throw new FileNotFoundException(ex.FileName);
            }
        }
    }
}
