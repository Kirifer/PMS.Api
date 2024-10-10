namespace Pms.Shared.Extensions
{
    public static class EnumerableExtension
    {
        /// <summary>
        /// Returns true when the enumerable items is null or empty
        /// </summary>
        /// <param name="enumerable">Enumerable item list to be checked</param>
        public static bool IsNullOrEmpty<TEntity>(
            this IEnumerable<TEntity> enumerable,
            Func<TEntity, bool>? predicate = null)
        {
            return Equals(enumerable, null) || !(Equals(predicate, null) ?
                enumerable.Any() :
                enumerable.Any(predicate));
        }

        /// <summary>
        /// Returns true when the collection items count is not zero
        /// </summary>
        /// <param name="enumerable">Collection item list to be checked</param>
        public static bool HasRecords<TEntity>(this ICollection<TEntity> collection)
        {
            return collection.Count != 0;
        }
    }
}