using ClassLibrary.Dtos.SharedDtos;
using HrTechniqueServer.Data;
using HrTechniqueServer.Exceptions;
using HrTechniqueServer.Models;
using Microsoft.EntityFrameworkCore;

namespace HrTechniqueServer.Repositories;

public interface IEmployeeRepository
    : IGettableRepo<Employee>,
        ISettableRepo<Employee>,
        IDeletableRepo<Employee>,
        IWritableRepo;

public sealed class EmployeeRepository(HrtDataContext hrtDataContext)
    : BaseRepository<Employee>(hrtDataContext),
        IEmployeeRepository
{
    public Employee Create(Employee employee)
    {
        HrtDataContext.Employees.Add(employee);
        return employee;
    }

    public Employee Update(Employee entity)
    {
        throw new NotImplementedException();
    }

    public async Task<Employee> GetByIdAsync(long id)
    {
        Employee employee =
            await HrtDataContext.Employees.FirstOrDefaultAsync(p => p.Id == id)
            ?? throw new EmployeeDoesNotExistException("employeeDoesNotExist");
        return employee;
    }

    public async Task<List<Employee>> GetAllAsync()
    {
        return await HrtDataContext.Employees.ToListAsync();
    }

    public List<Employee> DeleteMany(List<IdDto> products)
    {
        List<Employee> dbEmployees = HrtDataContext
            .Employees.ToList()
            .Where(dbP => products.Any(u => u.Id == dbP.Id))
            .ToList();
        HrtDataContext.Employees.RemoveRange(dbEmployees);
        return HrtDataContext.Employees.ToList();
    }
}
