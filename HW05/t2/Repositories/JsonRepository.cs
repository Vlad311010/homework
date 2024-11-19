using t2.Library.SerializationModels;
using t2.Library;
using System.Text.Json;

namespace t2.Repositories
{
    internal class JsonRepository : IRepository<Catalog>
    {
        readonly static JsonSerializerOptions serializerOptions = new JsonSerializerOptions() 
        { 
            WriteIndented = true,
            Converters = { new CatalogDictionaryConverter() } 
        };

        public void Serialize(Catalog catalog, string fileName)
        {
            CatalogSerializationModel catalogSM = new CatalogSerializationModel(catalog);

            using (var writer = new StreamWriter(fileName))
            {
                string jsonCatalog = JsonSerializer.Serialize<CatalogSerializationModel>(catalogSM, serializerOptions);
                writer.Write(jsonCatalog);                
            }
        }

        public Catalog Deserialize(string fileName)
        {
            try
            {
                using (var reader = new StreamReader(fileName))
                {
                    string json = reader.ReadToEnd();
                    CatalogSerializationModel? catalogSM = JsonSerializer.Deserialize<CatalogSerializationModel>(json, serializerOptions);
                    if (catalogSM == null)
                        throw new JsonException("Can't parse given json file");

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
