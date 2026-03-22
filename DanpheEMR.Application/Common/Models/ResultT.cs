public class Result<T>
{
    public bool Success { get; private set; }

    public T Data { get; private set; }

    public string Error { get; private set; }

    public static Result<T> Ok(T data)
    {
        return new Result<T>
        {
            Success = true,
            Data = data
        };
    }

    public static Result<T> Failure(string error)
    {
        return new Result<T>
        {
            Success = false,
            Error = error
        };
    }
}