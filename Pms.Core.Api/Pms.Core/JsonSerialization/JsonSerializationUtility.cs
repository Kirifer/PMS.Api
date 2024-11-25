using System.Text.Json;
using System.Text.Json.Serialization;

namespace Pms.Core.Extensions
{
    public class JsonSerializationUtility
    {
        /// <summary>
        /// Gets the JSON Serializer options
        /// </summary>
        /// <param name="existingSettings">Existing settings to be used or extend if provided</param>
        public static JsonSerializerOptions GetJsonSerializerOptions(JsonSerializerOptions existingOptions = null)
        {
            var options = Equals(existingOptions, null) ?
                new JsonSerializerOptions() : existingOptions;

            options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            //options.IgnoreNullValues = true;
            options.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

            // Add object converters other than declared types
            options.Converters.Add(new ObjectConverter());
            return options;
        }

        /// <summary>
        /// Deserializes dynamic data to object
        /// </summary>
        /// <param name="dynamicData">Dynamic input data</param>
        public static TObject DeserializeDynamicObject<TObject>(dynamic dynamicData)
            where TObject : new()
        {
            if (dynamicData is null) return new TObject();

            var options = GetJsonSerializerOptions();
            string rawJsonString = JsonSerializer.Serialize(dynamicData, options);
            return rawJsonString.DeserializeEntity<TObject>();
        }
    }
}
