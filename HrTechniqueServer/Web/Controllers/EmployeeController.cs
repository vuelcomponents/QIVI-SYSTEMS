using HrTechniqueServer.Dto;
using HrTechniqueServer.Features.Employee.Commands;
using HrTechniqueServer.Features.Employee.Queries;
using HrTechniqueServer.Filters;
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
    // [HttpGet("get/{id}")]
    // public async Task<ActionResult<EmployeeDto>> Get(int id)
    // {
    //     try
    //     {
    //         var query = new GetEmployeeByIdQuery(id);
    //         var result = await mediator.Send(query);
    //         return Ok(result);
    //     }
    //     catch (Exception e)
    //     {
    //         return BadRequest(e.Message);
    //     }
    // }
    //
    // [HttpGet("get")]
    // public async Task<ActionResult<List<EmployeeDto>>> GetAll()
    // {
    //     try
    //     {
    //         var query = new GetAllEmployeeQuery();
    //         var result = await mediator.Send(query);
    //         return Ok(result);
    //     }
    //     catch (Exception e)
    //     {
    //         return BadRequest(e.Message);
    //     }
    // }
    //
    // [HttpPost("create")]
    // public async Task<ActionResult<EmployeeDto>> Create(EmployeeDto employeeDto)
    // {
    //     try
    //     {
    //         var command = new CreateEmployeeCommand(employeeDto, (HttpContext.Items["User"] as UserShortDto)!);
    //         var result = await mediator.Send(command);
    //         return Ok(result);
    //     }
    //     catch (Exception e)
    //     {
    //         return BadRequest(e.Message);
    //     }
    // }
    // [HttpPatch("update")]
    // public async Task<ActionResult<EmployeeDto>> Update(EmployeeDto employee)
    // {
    //     try
    //     {
    //         var command = new UpdateEmployeeCommand(employee);
    //         var result = await mediator.Send(command);
    //         return Ok(result);
    //     }
    //     catch (Exception e)
    //     {
    //         throw new Exception(e.Message);
    //     }
    // }
    //
    // [HttpPost("delete-many")]
    // public async Task<IActionResult> DeleteMany(List<IdDto> employees)
    // {
    //     try
    //     {
    //         var command = new DeleteManyEmployeeCommand(employees);
    //         await mediator.Send(command);
    //         return Ok();
    //     }
    //     catch (Exception e)
    //     {
    //         throw new Exception(e.Message);
    //     }
    // }
}
