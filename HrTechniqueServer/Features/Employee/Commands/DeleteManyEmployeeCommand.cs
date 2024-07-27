using ClassLibrary.Dtos.SharedDtos;
using MediatR;

namespace HrTechniqueServer.Features.Employee.Commands;

public class DeleteManyEmployeeCommand(List<IdDto> employeesIdDtoList) : IRequest
{
    public List<IdDto> EmployeesIdDtoList { get; set; } = employeesIdDtoList;
}
