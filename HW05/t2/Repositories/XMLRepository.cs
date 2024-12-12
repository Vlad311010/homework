using System.Xml;
using System.Xml.Serialization;
using t2.LibraryModels;
using t2.LibraryModels.SerializationModels;
using t2.Repositories;

namespace t2.Serializers
{
    internal class XMLRepository : IRepository<Catalog>
    {
        private static readonly string _repositoryDataPath = @".\PersistentData\catalog.xml";
        private static readonly XmlSerializer _serializer = new XmlSerializer(typeof(CatalogSerializationModel));

        public void Serialize(Catalog catalog)
        {
            CatalogSerializationModel catalogSM = new CatalogSerializationModel(catalog);
            using (var writer = new StreamWriter(_repositoryDataPath))
            {
                _serializer.Serialize(writer, catalogSM);
            }
        }

        public Catalog Deserialize()
        {
            using (var reader = new StreamReader(_repositoryDataPath))
            {
                CatalogSerializationModel? catalogSM = _serializer.Deserialize(reader) as CatalogSerializationModel;
                if (catalogSM == null)
                    throw new XmlException("Can't parse given xml file");

                return catalogSM.AsCatalog();
            }
        }
    }
}
