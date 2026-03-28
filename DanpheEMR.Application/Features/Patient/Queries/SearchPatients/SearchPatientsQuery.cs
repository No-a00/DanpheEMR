using MediatR;

namespace DanpheEMR.Application.Features.Patients.Queries.SearchPatients
{
    public record SearchPatientsQuery(string SearchTerm) : IRequest<Result<List<SearchPatientsResponse>>>;
}