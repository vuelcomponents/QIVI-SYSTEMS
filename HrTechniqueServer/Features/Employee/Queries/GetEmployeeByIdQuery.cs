using HrTechniqueServer.Domain.Dto;
using MediatR;

namespace HrTechniqueServer.Features.Employee.Queries;

public class GetEmployeeByIdQuery(int id) : IRequest<EmployeeDto>
{
    public int Id { get; set; } = id;
}
