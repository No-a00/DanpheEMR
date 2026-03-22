public abstract class BasePagedQuery<T>
    : BaseQuery<T>
{
    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 10;
}