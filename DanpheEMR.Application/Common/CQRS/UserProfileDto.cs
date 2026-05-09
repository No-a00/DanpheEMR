

using DanpheEMR.Core.Domain.Admin;

namespace DanpheEMR.Application.Common.Models
{
    public class UserProfileDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        
        public string ? AvatarUrl { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }

        public bool IsActive { get; set; }
        public List<Permission> Permissions { get; set; } = new();
    }
}