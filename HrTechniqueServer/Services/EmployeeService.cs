using AutoMapper;
using ClassLibrary.Dtos.Auth;
using ClassLibrary.Dtos.SharedDtos;
using ClassLibrary.Utils;
using HrTechniqueServer.Dto;
using HrTechniqueServer.Models;
using HrTechniqueServer.Repositories;

namespace HrTechniqueServer.Services;

public sealed class EmployeeService(
    IEmployeeRepository employeeRepository,
    IUserRepository userRepository,
    IQuickActions quickActions,
    IMapper mapper
) : IEmployeeService
{
    public async Task<EmployeeDto> Get(int id)
    {
        Employee employee = await employeeRepository.GetByIdAsync(id);
        EmployeeDto employeeDto = mapper.Map<EmployeeDto>(employee);

        employeeDto.User = await userRepository.GetUserById(employee.UserId);

        return employeeDto;
    }

    public async Task<List<EmployeeDto>> Get()
    {
        List<Employee> employees = await employeeRepository.GetAllAsync();
        return employees.Select(mapper.Map<EmployeeDto>).ToList();
    }

    public async Task<EmployeeDto> Create(UserShortDto user, EmployeeDto employeeDto)
    {
        Employee initEmployee = mapper.Map<Employee>(employeeDto);
        initEmployee.UserId = user.Id;

        Employee createdEmployee = employeeRepository.Create(initEmployee);
        await employeeRepository.SaveAsync();
        EmployeeDto createdEmployeeDto = mapper.Map<EmployeeDto>(createdEmployee);
        createdEmployeeDto.User = await userRepository.GetUserById(createdEmployee.UserId);

        return createdEmployeeDto;
    }

    public async Task<EmployeeDto> Update(UserShortDto user, EmployeeDto employeeDto)
    {
        Employee updateEmployee = await employeeRepository.GetByIdAsync(employeeDto.Id);
        quickActions.QuickUpdate(employeeDto, updateEmployee, ["Email", "Licences", "Block"]);
        await employeeRepository.SaveAsync();

        EmployeeDto updatedEmployeeDto = mapper.Map<EmployeeDto>(updateEmployee);
        updatedEmployeeDto.User = await userRepository.GetUserById(updateEmployee.UserId);

        return updatedEmployeeDto;
    }

    public async Task<List<EmployeeDto>> DeleteMany(List<IdDto> products)
    {
        List<Employee> employees = employeeRepository.DeleteMany(products);
        await employeeRepository.SaveAsync();
        return employees.Select(mapper.Map<EmployeeDto>).ToList();
    }
}
