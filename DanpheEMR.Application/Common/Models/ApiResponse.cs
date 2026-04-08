using Application.Common;
namespace DanpheEMR.Application.Common.Models
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
        public List<Error> Errors { get; set; } = new();

        public static ApiResponse<T> Success(T data, string message = "Thành công")
        {
            return new ApiResponse<T> { IsSuccess = true, Data = data, Message = message };
        }

        public static ApiResponse<T> Failure(List<Error> errors, string message = "Thất bại")
        {
            return new ApiResponse<T> { IsSuccess = false, Errors = errors, Message = message };
        }
    }
}