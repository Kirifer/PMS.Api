using System.Collections;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

using Pms.Shared.Extensions;

namespace Pms.Core.Filtering
{
    public class FilterBase
    {
        [JsonIgnore]
        public Guid? Id { get; set; }

        [JsonIgnore]
        public List<Guid> Ids { get; set; }

        public FilterBase()
        {
            Ids = new List<Guid>();
        }

        #region Query parameter conversion

        /// <summary>
        /// Converts the filter into equivalent query parameter
        /// </summary>
        /// <returns>The converted query paremeters.</returns>
        public string ToQueryParameter()
        {
            var queryString = new StringBuilder();
            var values = GetType().GetProperties()
                .Where(prop => prop.GetValue(this, null) != null)
                .Select(prop => ConvertToQueryParameter(prop, this));

            return values.IsNullOrEmpty() ? string.Empty : $"?{string.Join("&", values)}";
        }

        private string ConvertToQueryParameter(PropertyInfo property, object obj)
        {
            if (property.PropertyType != typeof(string) && typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
            {
                var objList = (IList?)property.GetValue(obj);
                if (objList == null) return string.Empty;

                var values = new List<string>();
                foreach (var item in objList)
                    values.Add($"{property.Name}={item}");

                return string.Join("&", values);
            }
            else if (property.PropertyType == typeof(Paging))
            {
                var classProps = property.PropertyType.GetProperties();
                var classObj = property.GetValue(obj);

                var values = new List<string>();
                foreach (var prop in classProps)
                    values.Add($"{property.Name}.{prop.Name}={prop.GetValue(classObj)}");

                return string.Join("&", values);
            }
            else
                return $"{property.Name}={property.GetValue(obj)}";
        }

        #endregion
    }

    public class FullFilterBase : FilterBase
    {
        public Paging? Paging { get; set; }

        public Sorting? Sorting { get; set; }
    }
}
