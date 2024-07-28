using HrTechniqueServer.Domain.Dto;
using MediatR;

namespace HrTechniqueServer.Features.Employee.Commands;

public class UpdateEmployeeCommand(EmployeeDto employeeDto) : IRequest<EmployeeDto>
{
    public EmployeeDto EmployeeDto { get; set; } = employeeDto;
}
