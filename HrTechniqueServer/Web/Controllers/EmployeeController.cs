using HrTechniqueServer.Domain.Dto;
using HrTechniqueServer.Features.Employee.Commands;
using HrTechniqueServer.Features.Employee.Queries;
using HrTechniqueServer.Infrastructure.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HrTechniqueServer.Web.Controllers;

[ApiController]
[AuthorizedMicro]
[Route("employee")]
public sealed class EmployeeController(IMediator mediator)
    : BaseCrudController<
        EmployeeDto,
        CreateEmployeeCommand,
        UpdateEmployeeCommand,
        DeleteManyEmployeeCommand,
        GetEmployeeByIdQuery,
        GetAllEmployeeQuery
    >(mediator)
{
    /* Mediator Pattern before generics -> Vertical Slices 
        [HttpGet("get/{id}")]
        public async Task<ActionResult<EmployeeDto>> Get(int id)
        {
            try
            {
                var query = new GetEmployeeByIdQuery(id);
                var result = await mediator.Send(query);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    */
}
