namespace Pms.Core.Filtering
{
    public interface IPaging
    {
        int Page { get; set; }

        int PageSize { get; set; }
    }
}
