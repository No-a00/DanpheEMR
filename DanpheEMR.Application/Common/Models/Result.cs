public class Result
{
    public bool Success { get; private set; }

    public string Error { get; private set; }

    public static Result Ok()
    {
        return new Result
        {
            Success = true
        };
    }

    public static Result Failure(string error)
    {
        return new Result
        {
            Success = false,
            Error = error
        };
    }
}