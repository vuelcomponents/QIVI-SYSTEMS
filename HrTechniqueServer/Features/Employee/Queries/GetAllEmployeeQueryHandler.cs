using AutoMapper;
using HrTechniqueServer.Domain.Dto;
using HrTechniqueServer.Infrastructure.Persistence.Repositories;
using MediatR;

namespace HrTechniqueServer.Features.Employee.Queries;

public class GetAllEmployeeQueryHandler(EmployeeRepository employeeRepository, IMapper mapper)
    : IRequestHandler<GetAllEmployeeQuery, List<EmployeeDto>>
{
    public async Task<List<EmployeeDto>> Handle(
        GetAllEmployeeQuery request,
        CancellationToken cancellationToken
    )
    {
        List<Domain.Entities.Employee> employees = await employeeRepository.GetAllAsync();
        return employees.Select(mapper.Map<EmployeeDto>).ToList();
    }
}
