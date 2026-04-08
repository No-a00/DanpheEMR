//403

namespace DanpheEMR.Application.Exceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException() : base("Bạn không có quyền thực hiện hành động này.") { }

        public ForbiddenException(string message) : base(message) { }
    }
}