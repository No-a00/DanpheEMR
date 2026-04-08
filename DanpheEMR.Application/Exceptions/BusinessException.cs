//400 || 422 
namespace DanpheEMR.Application.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException() : base("Yêu cầu không hợp lệ do vi phạm quy tắc nghiệp vụ.") { }

        public BusinessException(string message) : base(message) { }

        public BusinessException(string message, Exception innerException) : base(message, innerException) { }
    }
}