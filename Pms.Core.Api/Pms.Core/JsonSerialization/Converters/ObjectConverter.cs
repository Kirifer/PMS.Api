using System.Text.Json;
using System.Text.Json.Serialization;

namespace Pms.Core.Extensions
{
    public class ObjectConverter : JsonConverter<object>
    {
        public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Convert String
            if (reader.TokenType == JsonTokenType.String)
            {
                return reader.GetString();
            }

            // Convert number type
            if (reader.TokenType == JsonTokenType.Number)
            {
                var convertResult = reader.TryGetInt32(out var intResult);
                if (convertResult) return intResult;

                convertResult = reader.TryGetInt64(out var longResult);
                if (convertResult) return longResult;

                convertResult = reader.TryGetDouble(out var doubleResult);
                if (convertResult) return doubleResult;
                return reader.GetInt16();
            }

            // Convert Boolean types
            if (reader.TokenType == JsonTokenType.True)
            {
                return true;
            }
            if (reader.TokenType == JsonTokenType.False)
            {
                return false;
            }

            // Convert array values
            if (reader.TokenType == JsonTokenType.StartArray)
            {
                var itemList = new List<KeyValuePair<string, object>>();
                while (reader.Read())
                {
                    if (reader.TokenType == JsonTokenType.EndObject ||
                        reader.TokenType == JsonTokenType.EndArray)
                    {
                        return itemList;
                    }

                    var dictionaryValue = JsonSerializer.Deserialize<Dictionary<string, object>>(ref reader, options);
                    foreach (var item in dictionaryValue)
                    {
                        itemList.Add(new KeyValuePair<string, object>(item.Key, item.Value));
                    }
                }
                return itemList;
            }

            using JsonDocument document = JsonDocument.ParseValue(ref reader);
            return document.RootElement.Clone();
        }

        public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }
    }
}
