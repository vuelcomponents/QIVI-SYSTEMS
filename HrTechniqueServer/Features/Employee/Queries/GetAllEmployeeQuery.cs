using HrTechniqueServer.Domain.Dto;
using MediatR;

namespace HrTechniqueServer.Features.Employee.Queries;

public class GetAllEmployeeQuery : IRequest<List<EmployeeDto>>;
