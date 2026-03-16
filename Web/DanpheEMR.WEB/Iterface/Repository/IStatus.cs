namespace DanpheEMR.WEB.Iterface.Repository
{
    public enum Gender
    {
        Male = 1,
        Female = 2,
        Other = 3
    }

    public enum PatientStatus
    {
        New = 0,            // Mới đăng ký, chưa khám
        Waiting = 1,        // Đang chờ ở phòng khám
        Admitted = 2,       // Đang nằm viện (Nội trú)
        Discharged = 3,     // Đã xuất viện
        Transferred = 4     // Đã chuyển viện khác
    }
}