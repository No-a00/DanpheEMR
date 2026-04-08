using System.ComponentModel.DataAnnotations;

namespace DanpheEMR.Core.Domain.Base
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
        [Required]
        string Reason { get; set; }
        [Required]
        Guid? DeletedBy { get; set; }
    }
}