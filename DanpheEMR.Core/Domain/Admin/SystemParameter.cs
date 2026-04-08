using DanpheEMR.Core.Domain.Base;

namespace DanpheEMR.Core.Domain.Admin
{
    public class SystemParameter : BaseEntity
    {
        public Guid Id { get; set; }
        public string ParameterName { get; set; }
        public string ParameterValue { get; set; }
        public string ValueType { get; set; } 
    }
}
