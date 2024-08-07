using System.Text;
using System.Text.RegularExpressions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Pms.Core.Database
{
    public static class DbExtensions
    {
        /// <summary>
        /// Converts current string to snake-case
        /// </summary>
        /// <param name="input">Input string to be converted</param>
        public static string ToSnakeCase(this string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return input;

            var startUnderscores = Regex.Match(input, @"^_+");
            return $"{startUnderscores}{Regex.Replace(input, @"([a-z0-9])([A-Z])", "$1_$2").ToLower()}";
        }

        /// <summary>
        /// Converts current string to PascalCase
        /// </summary>
        /// <param name="input">The raw string format.</param>
        /// <returns>The converted PascalCase</returns>
        public static string ToPascalCase(this string input)
        {
            var result = new StringBuilder();
            var nonWordChars = new Regex(@"[^a-zA-Z0-9]+");
            var tokens = nonWordChars.Split(input);
            foreach (var token in tokens)
            {
                result.Append(PascalCaseSingleWord(token));
            }

            return result.ToString();
        }

        private static string PascalCaseSingleWord(string s)
        {
            var match = Regex.Match(s, @"^(?<word>\d+|^[a-z]+|[A-Z]+|[A-Z][a-z]+|\d[a-z]+)+$");
            var groups = match.Groups["word"];

            var textInfo = Thread.CurrentThread.CurrentCulture.TextInfo;
            var result = new StringBuilder();
            foreach (var capture in groups.Captures.Cast<Capture>())
            {
                result.Append(textInfo.ToTitleCase(capture.Value.ToLower()));
            }
            return result.ToString();
        }

        /// <summary>
        /// Use snake_case in all entity attributes
        /// </summary>
        public static void UseSnakeCase(this ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entity.GetTableName()!.ToSnakeCase();
                var schema = entity.GetSchema();
                var storeObjectIdentifier = StoreObjectIdentifier.Table(tableName, schema);

                entity.SetTableName(tableName);

                foreach (var property in entity.GetProperties())
                    property.SetColumnName(property.GetColumnName(storeObjectIdentifier)!.ToSnakeCase());

                foreach (var key in entity.GetKeys())
                    key.SetName(key.GetName()!.ToSnakeCase());

                foreach (var key in entity.GetForeignKeys())
                    key.SetConstraintName(key.GetConstraintName()!.ToSnakeCase());

                foreach (var index in entity.GetIndexes())
                    index.SetDatabaseName(index.GetDatabaseName()!.ToSnakeCase());
            }
        }

        /// <summary>
        /// Registers all entity configuration within the assembly
        /// </summary>
        public static void RegisterEntityConfigurations(this ModelBuilder modelBuilder)
        {
            var targetAssembly = AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(item => item.GetTypes().Any(itemType => itemType.GetInterface(typeof(IEntityTypeConfiguration<>).Name) != null && itemType.IsClass))
                .FirstOrDefault();
            modelBuilder.ApplyConfigurationsFromAssembly(targetAssembly!);
        }
    }
}
