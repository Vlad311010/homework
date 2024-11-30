using System.Text.Json.Serialization;
using System.Text.Json;
using t2.Library.SerializationModels;
using t2.Library;

namespace t2
{
    /// <summary>
    /// JSON does not natively support non-string dictionary keys, so i had to implement Dictionary<ISBN13, BookSerializationModel> converter
    /// </summary>
    public class CatalogDictionaryConverter : JsonConverter<Dictionary<ISBN13, BookSerializationModel>>
    {
        public override Dictionary<ISBN13, BookSerializationModel> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected start of an object.");
            }

            var dictionary = new Dictionary<ISBN13, BookSerializationModel>();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return dictionary;
                }

                ISBN13 key = new ISBN13(reader.GetString()!);
                
                // Desirialize book value
                reader.Read();
                BookSerializationModel value = JsonSerializer.Deserialize<BookSerializationModel>(ref reader, options)!;

                dictionary.Add(key, value);
            }

            return dictionary;
        }

        public override void Write(Utf8JsonWriter writer, Dictionary<ISBN13, BookSerializationModel> value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            foreach (var kvp in value)
            {
                // Write key
                writer.WritePropertyName((string)kvp.Key);
                // Write value (Book)
                JsonSerializer.Serialize(writer, kvp.Value, options);
            }
            writer.WriteEndObject();
        }
    }
}
