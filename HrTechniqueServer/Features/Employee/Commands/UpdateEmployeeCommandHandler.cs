using AutoMapper;
using ClassLibrary.Utils;
using HrTechniqueServer.Dto;
using HrTechniqueServer.Exceptions;
using HrTechniqueServer.Infrastructure.Persistence.Repositories;
using MediatR;

namespace HrTechniqueServer.Features.Employee.Commands;

public class UpdateEmployeeCommandHandler(
    EmployeeRepository employeeRepository,
    IUserRepository userRepository,
    IQuickActions quickActions,
    IMapper mapper
) : IRequestHandler<UpdateEmployeeCommand, EmployeeDto>
{
    public async Task<EmployeeDto> Handle(
        UpdateEmployeeCommand request,
        CancellationToken cancellationToken
    )
    {
        Domain.Entities.Employee? updateEmployee = await employeeRepository.GetAsync(e =>
            e.Id.Equals(request.EmployeeDto.Id)
        );
        if (updateEmployee == null)
        {
            throw new EmployeeDoesNotExistException();
        }
        quickActions.QuickUpdate(
            request.EmployeeDto,
            updateEmployee,
            ["Email", "Licences", "Block"]
        );
        await employeeRepository.SaveAsync();

        EmployeeDto updatedEmployeeDto = mapper.Map<EmployeeDto>(updateEmployee);
        updatedEmployeeDto.User = await userRepository.GetUserById(updateEmployee.UserId);

        return updatedEmployeeDto;
    }
}
