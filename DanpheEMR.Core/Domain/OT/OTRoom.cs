using DanpheEMR.Core.Domain.Base;

namespace DanpheEMR.Core.Domain.OT
{
    public class OTRoom : BaseEntity
    {
        public int Id { get; set; }
        public string RoomName { get; set; }
        public string Location { get; set; }
        public bool IsAvailable { get; set; }

    }
}
