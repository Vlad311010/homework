using System.Xml;
using System.Xml.Serialization;
using t2.Library;
using t2.Library.SerializationModels;
using t2.Repositories;

namespace t2.Serializers
{
    internal class XMLRepository : IRepository<Catalog>
    {
        private static readonly string repositoryDataPath = @".\PersistentData\catalog.xml";
        private static readonly XmlSerializer Serializer = new XmlSerializer(typeof(CatalogSerializationModel));

        public void Serialize(Catalog catalog)
        {
            CatalogSerializationModel catalogSM = new CatalogSerializationModel(catalog);
            using (var writer = new StreamWriter(repositoryDataPath))
            {
                Serializer.Serialize(writer, catalogSM);
            }
        }

        public Catalog Deserialize()
        {
            using (var reader = new StreamReader(repositoryDataPath))
            {
                CatalogSerializationModel? catalogSM = Serializer.Deserialize(reader) as CatalogSerializationModel;
                if (catalogSM == null)
                    throw new XmlException("Can't parse given xml file");

                return catalogSM.AsCatalog();
            }
        }
    }
}
