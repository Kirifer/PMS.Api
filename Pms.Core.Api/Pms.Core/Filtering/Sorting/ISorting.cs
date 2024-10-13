namespace Pms.Core.Filtering
{
    public interface ISorting
    {
        string? SortBy { get; set; }

        string? Direction { get; set; }
    }
}
