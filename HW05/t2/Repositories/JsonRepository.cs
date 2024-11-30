using t2.Library.SerializationModels;
using t2.Library;
using System.Text.Json;

namespace t2.Repositories
{
    internal class JsonRepository : IRepository<Catalog>
    {
        readonly static string repositoryDataPath = @".\PersistentData\catalog.json";

        readonly static JsonSerializerOptions serializerOptions = new JsonSerializerOptions() 
        { 
            WriteIndented = true,
            Converters = { new CatalogDictionaryConverter() } 
        };

        public void Serialize(Catalog catalog)
        {
            CatalogSerializationModel catalogSM = new CatalogSerializationModel(catalog);

            using (var writer = new StreamWriter(repositoryDataPath))
            {
                string jsonCatalog = JsonSerializer.Serialize<CatalogSerializationModel>(catalogSM, serializerOptions);
                writer.Write(jsonCatalog);                
            }
        }

        public Catalog Deserialize()
        {
            using (var reader = new StreamReader(repositoryDataPath))
            {
                string json = reader.ReadToEnd();
                CatalogSerializationModel? catalogSM = JsonSerializer.Deserialize<CatalogSerializationModel>(json, serializerOptions);
                if (catalogSM == null)
                    throw new JsonException("Can't parse given json file");

                return catalogSM.AsCatalog();
            }
        }
    }
}
