using HrTechniqueServer.Domain.Entities;
using HrTechniqueServer.Infrastructure.Persistence.Data;

namespace HrTechniqueServer.Infrastructure.Persistence.Repositories;

public interface IEmployeeRepository;

public sealed class EmployeeRepository(HrtDataContext hrtDataContext)
    : BaseRepository<Employee>(hrtDataContext),
        IEmployeeRepository { }
