using System.Text.Json;

namespace Pms.Core.Extensions
{
    public static class JsonSerializationExtension
    {
        /// <summary>
        /// Serialize the entity using JSON Text
        /// </summary>
        /// <typeparam name="TEntity">The Entity Type to be serialized</typeparam>
        /// <param name="entity">Entity to be serialized</param>
        public static string SerializeEntity<TEntity>(this TEntity entity)
        {
            var options = JsonSerializationUtility.GetJsonSerializerOptions();
            return JsonSerializer.Serialize<TEntity>(entity, options);
        }

        /// <summary>
        /// Deserialize the entity using JSON Text
        /// </summary>
        /// <typeparam name="TEntity">The Entity Type to be deserialized</typeparam>
        /// <param name="entity">Entity to be deserialized</param>
        public static TEntity? DeserializeEntity<TEntity>(this string jsonString)
        {
            if (string.IsNullOrWhiteSpace(jsonString))
                return Activator.CreateInstance<TEntity>();

            var options = JsonSerializationUtility.GetJsonSerializerOptions();
            return JsonSerializer.Deserialize<TEntity>(jsonString, options);
        }

        /// <summary>
        /// Deserialize the entity using JSON Text
        /// </summary>
        /// <param name="jsonString">Entity to be deserialized</param>
        public static object? DeserializeEntity(this string jsonString, Type type)
        {
            if (string.IsNullOrWhiteSpace(jsonString))
                return Activator.CreateInstance<object>();

            var options = JsonSerializationUtility.GetJsonSerializerOptions();
            return JsonSerializer.Deserialize(jsonString, type, options);
        }

        /// <summary>
        /// Try to deserialize the entity and return true if the deserialization was successful, otherwise false
        /// </summary>
        /// <param name="jsonString">Json string to be deserialized</param>
        /// <param name="deserialized">Deserialized items</param>
        public static bool TryDeserializeEntity<TEntity>(this string jsonString, out TEntity? deserialized)
        {
            try
            {
                deserialized = jsonString.DeserializeEntity<TEntity>();
                return true;
            }
            catch (Exception)
            {
                deserialized = default;
                return false;
            }
        }
    }
}
