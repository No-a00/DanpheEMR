//404
namespace DanpheEMR.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException() : base("Không tìm thấy dữ liệu yêu cầu.") { }

        public NotFoundException(string message) : base(message) { }

        public NotFoundException(string message, Exception innerException) : base(message, innerException) { }
        public NotFoundException(string name, object key)
            : base($"Không tìm thấy bản ghi thuộc '{name}' với khóa '{key}'.") { }
    }
}