public abstract class BaseCacheableQuery<T>
    : BaseQuery<T>
{
    public abstract string CacheKey { get; }

    public virtual int SlidingExpiration
        => 5;
}