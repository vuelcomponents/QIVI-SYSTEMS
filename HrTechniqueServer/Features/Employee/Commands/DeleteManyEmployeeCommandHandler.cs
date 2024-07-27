using HrTechniqueServer.Exceptions;
using HrTechniqueServer.Infrastructure.Persistence.Repositories;
using MediatR;

namespace HrTechniqueServer.Features.Employee.Commands;

public class DeleteManyEmployeeCommandHandler(EmployeeRepository employeeRepository)
    : IRequestHandler<DeleteManyEmployeeCommand>
{
    public Task Handle(DeleteManyEmployeeCommand request, CancellationToken cancellationToken)
    {
        return Task.Run(
                async () =>
                {
                    await employeeRepository.DeleteManyByIdDtoList(request.EmployeesIdDtoList);
                },
                cancellationToken
            )
            .ContinueWith(
                task =>
                {
                    if (task.IsCompletedSuccessfully)
                    {
                        return employeeRepository.SaveAsync();
                    }
                    return Task.FromException<SaveException>(new SaveException());
                },
                cancellationToken
            );
    }
}
