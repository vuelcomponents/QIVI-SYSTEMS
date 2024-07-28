using AutoMapper;
using HrTechniqueServer.Domain.Dto;
using HrTechniqueServer.Domain.Exceptions;
using HrTechniqueServer.Infrastructure.Persistence.Repositories;
using MediatR;

namespace HrTechniqueServer.Features.Employee.Queries;

public class GetEmployeeByIdQueryHandler(EmployeeRepository employeeRepository, IMapper mapper)
    : IRequestHandler<GetEmployeeByIdQuery, EmployeeDto>
{
    public async Task<EmployeeDto> Handle(
        GetEmployeeByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        var employee = await employeeRepository.GetAsync(e => e.Id.Equals(request.Id));

        if (employee == null)
        {
            throw new EmployeeDoesNotExistException($"Employee with ID {request.Id} not found.");
        }

        return mapper.Map<EmployeeDto>(employee);
    }
}
