using t2.Library.SerializationModels;
using t2.Library;
using System.Text.Json;

namespace t2.Repositories
{
    internal class JsonRepository : IRepository<Catalog>
    {
        // json can't serialize tuples so i defined a record type
        private record BookData(BookSerializationModel Book, string ISBN);

        private class JsonSerializationType
        {
            public AuthorSerializationModel Author { get; set; }
            public BookData[] Books { get; set; }
        }


        readonly static string repositoryDataPath = @".\PersistentData\JsonCatalog\";
        readonly static string fileNaming = "{0} books.json";

        readonly static JsonSerializerOptions serializerOptions = new JsonSerializerOptions() 
        { 
            WriteIndented = true,
        };

        public void Serialize(Catalog catalog)
        {
            Directory.CreateDirectory(repositoryDataPath);

            foreach (Author author in catalog.GetAuthors()) 
            {
                var authorBooks = catalog.WrittenByWithISBN(author).ToArray();

                string filePath = Path.Combine(repositoryDataPath, string.Format(fileNaming, author.FullName));

                var serializationData = new JsonSerializationType
                {
                    Author = new AuthorSerializationModel(author),
                    Books = authorBooks.Select(bookData => new BookData(new BookSerializationModel(bookData.Item1), (string)bookData.Item2)).ToArray()
                };

                using (var writer = new StreamWriter(filePath))
                {
                    string jsonCatalog = JsonSerializer.Serialize<JsonSerializationType>(serializationData, serializerOptions);
                    writer.Write(jsonCatalog);
                }
            }
        }

        public Catalog Deserialize()
        {
            var serializationDataType = new { author = string.Empty, books = Array.Empty<string>() };

            if (!Directory.Exists(repositoryDataPath))
                throw new FileNotFoundException("No data to deserialize");

            string[] files = Directory.GetFiles(repositoryDataPath, "*.json");

            Catalog catalog = new Catalog();
            foreach (string file in files)
            {
                using (var reader = new StreamReader(file))
                {
                    string json = reader.ReadToEnd();
                    JsonSerializationType? result = JsonSerializer.Deserialize<JsonSerializationType>(json, options: serializerOptions);
                    if (result == null)
                        throw new JsonException($"Can't parse given json file. {file}");

                    foreach (BookData BookData in result.Books)
                    {
                        catalog[BookData.ISBN] = BookData.Book.AsBook();
                    }
                }
            }
            
            return catalog;
        }
    }
}
