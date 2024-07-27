using AutoMapper;
using HrTechniqueServer.Dto;
using HrTechniqueServer.Infrastructure.Persistence.Repositories;
using MediatR;

namespace HrTechniqueServer.Features.Employee.Commands;

public class CreateEmployeeCommandHandler(
    EmployeeRepository employeeRepository,
    IUserRepository userRepository,
    IMapper mapper
) : IRequestHandler<CreateEmployeeCommand, EmployeeDto>
{
    public async Task<EmployeeDto> Handle(
        CreateEmployeeCommand request,
        CancellationToken cancellationToken
    )
    {
        Domain.Entities.Employee initEmployee = mapper.Map<Domain.Entities.Employee>(
            request.EmployeeDto
        );
        initEmployee.UserId = request.UserShortDto.Id;

        Domain.Entities.Employee createdEmployee = employeeRepository.Create(initEmployee);
        await employeeRepository.SaveAsync();
        EmployeeDto createdEmployeeDto = mapper.Map<EmployeeDto>(createdEmployee);
        createdEmployeeDto.User = await userRepository.GetUserById(createdEmployee.UserId);

        return createdEmployeeDto;
    }
}
