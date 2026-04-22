using Application.Common;
using System.Text.Json.Serialization;

public class Result<TValue> : Result
{
    private readonly TValue? _value;

    protected internal Result(TValue? value, bool isSuccess, Error error)
        : base(isSuccess, error)
    {
        _value = value;
    }
    [JsonIgnore]
    public TValue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("Không thể lấy giá trị của một Result thất bại.");

    // Chỉnh sửa hàm Failure ở đây nữa
    public static new Result<TValue> Failure(Error error) => new(default, false, error);
}