namespace Pms.Shared.Extensions
{
    public static class DateOnlyUtility
    {
        /// <summary>
        /// Converts the year
        /// </summary>
        /// <param name="dateTimeString">DateTime string to be converted</param>
        public static DateOnly ConvertNumberYearToDateOnly(int year, int month = 1, int day = 1)
            => DateOnly.FromDateTime(new DateTime(year, month, day));
    }
}
