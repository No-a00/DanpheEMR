namespace DanpheEMR.Core.Enums;

public enum AdmissionStatus
{
    Active = 1,
    Discharged = 2,
    Cancelled = 3
}

public enum BedStatus
{
    Available = 1,
    Occupied = 2,
    Maintenance = 3,
    Reserved = 4
}

public enum GoodsReceiptStatus
{
    Pending = 1,
    Received = 2,
    Cancelled = 3
}

public enum OTStatus
{
    Scheduled = 1,
    InProgress = 2,
    Completed = 3,
    Cancelled = 4
}

public enum PaymentStatus
{
    Pending = 1,
    Paid = 2,
    Refunded = 3,
    Cancelled = 4
}

public enum Status
{
    Inactive = 0,
    Active = 1
}

public enum TransactionType
{
    Sales = 1,
    Export = 2,
    import = 3
}

public enum TransferStatus
{
    Pending = 1,
    Approved = 2,
    Rejected = 3,
    Completed = 4
}
public enum VisitStatus
{
    Registered = 1,      // Đã đăng ký khám

    Waiting = 2,         // Đang chờ khám

    InConsultation = 3,  // Đang khám

    InTreatment = 4,     // Đang điều trị

    Completed = 5,       // Hoàn thành

    Cancelled = 6,       // Hủy

    NoShow = 7           // Không đến
}
public enum PaymentMode

{
    Cash = 1,
    Card = 2,
    DigitalWallets = 3,
    Transfer = 4

}

public enum BloodType
{
    APos = 1,         // A+
    ANeg = 2,         // A-
    BPos = 3,         // B+
    BNeg = 4,         // B-
    ABPos = 5,        // AB+
    ABNeg = 6,        // AB-
    OPos = 7,         // O+
    ONeg = 8          // O-
}