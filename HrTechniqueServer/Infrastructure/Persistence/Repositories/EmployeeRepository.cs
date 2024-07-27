using HrTechniqueServer.Domain.Entities;

namespace HrTechniqueServer.Infrastructure.Persistence.Repositories;

public interface IEmployeeRepository;

public sealed class EmployeeRepository(HrtDataContext hrtDataContext)
    : BaseRepository<Employee>(hrtDataContext),
        IEmployeeRepository { }
