using HrTechniqueServer.Dto;
using MediatR;

namespace HrTechniqueServer.Features.Employee.Queries;

public class GetAllEmployeeQuery : IRequest<List<EmployeeDto>>;
