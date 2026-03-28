using System;

namespace DanpheEMR.Application.Features.Admin.Queries.GetEmployees
{
    public class GetEmployeesResponse
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public string ContactNumber { get; set; }
        public bool IsActive { get; set; }
        public string DepartmentName { get; set; }
    }
}