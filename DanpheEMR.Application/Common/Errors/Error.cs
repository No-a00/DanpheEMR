namespace Application.Common;

public class Error
{
    public string Code { get; }


    public string Message { get; }

    public static readonly Error None = new(string.Empty, string.Empty);

    public static readonly Error NullValue = new("Error.NullValue", "Giá trị truyền vào bị null.");
    
    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }

}