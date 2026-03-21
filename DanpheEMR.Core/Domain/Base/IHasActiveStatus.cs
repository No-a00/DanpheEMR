namespace DanpheEMR.Core.Domain.Base
{
    // Bất kỳ bảng nào muốn dùng tính năng Xóa mềm thì kế thừa cái này
    public interface IHasActiveStatus
    {
        bool IsActive { get; set; }
    }
}