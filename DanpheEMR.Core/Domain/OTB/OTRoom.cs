using DanpheEMR.Core.Domain.Base;

namespace DanpheEMR.Core.Domain.Appointment
{
    public class OTRoom : BaseEntity
    {
        public int Id { get; set; }
        public string RoomName { get; set; }
        public string Loation { get; set; }
        public bool IsAvailable { get; set; }

    }
}
