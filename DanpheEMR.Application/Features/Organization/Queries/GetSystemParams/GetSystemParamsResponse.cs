using System;

namespace DanpheEMR.Application.Features.Admin.Queries.GetSystemParams
{
    public class GetSystemParamsResponse
    {
        public Guid Id { get; set; }
        public string ParameterName { get; set; }
        public string ParameterValue { get; set; }
        public string ValueType { get; set; }
    }
}