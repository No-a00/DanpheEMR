using System;
using System.Collections.Generic;

namespace DanpheEMR.Application.Features.Admin.Queries.GetDepartmentTree
{
    public class GetDepartmentTreeResponse
    {
        public Guid Id { get; set; }
        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }
        public bool IsClinical { get; set; }
        public bool IsActive { get; set; }
        public Guid? ParentDepartmentId { get; set; }
        public List<GetDepartmentTreeResponse> SubDepartments { get; set; } = new();
    }
}