using ClassLibrary.Dtos.Auth;
using HrTechniqueServer.Domain.Dto;
using MediatR;

namespace HrTechniqueServer.Features.Employee.Commands;

public class CreateEmployeeCommand(EmployeeDto employeeDto, UserShortDto userShortDto)
    : IRequest<EmployeeDto>
{
    public UserShortDto UserShortDto { get; set; } = userShortDto;
    public EmployeeDto EmployeeDto { get; set; } = employeeDto;
}
