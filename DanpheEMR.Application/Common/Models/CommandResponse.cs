
namespace DanpheEMR.Application.Common.Models
{
    public class CommandResponse
    {
        public Guid Id { get; set; }
        public string Message { get; set; }

        public CommandResponse(Guid id, string message = "Thao tác thành công!")
        {
            Id = id;
            Message = message;
        }
    }
}