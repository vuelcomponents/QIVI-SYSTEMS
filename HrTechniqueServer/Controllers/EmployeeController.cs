using ClassLibrary.Dtos;
using ClassLibrary.Dtos.Auth;
using ClassLibrary.Dtos.SharedDtos;
using HrTechniqueServer.Attributes;
using HrTechniqueServer.Dto;
using HrTechniqueServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace HrTechniqueServer.Controllers;

[ApiController]
[AuthorizedMicro]
[Route("employee")]
public sealed class EmployeeController(IEmployeeService employeeService)
    : ControllerBase,
        IBaseController<EmployeeDto>
{
    [HttpPost("create")]
    public async Task<ActionResult<EmployeeDto>> Create(EmployeeDto employeeDto)
    {
        try
        {
            return Ok(
                await employeeService.Create(
                    (HttpContext.Items["User"] as UserShortDto)!,
                    employeeDto
                )
            );
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("get/{id}")]
    public async Task<ActionResult<EmployeeDto>> Get(int id)
    {
        try
        {
            return Ok(await employeeService.Get(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("get")]
    public async Task<ActionResult<List<EmployeeDto>>> GetAll()
    {
        try
        {
            return Ok(await employeeService.Get());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPatch("update")]
    public async Task<ActionResult<EmployeeDto>> Update(EmployeeDto employee)
    {
        try
        {
            return Ok(
                await employeeService.Update((HttpContext.Items["User"] as UserShortDto)!, employee)
            );
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    [HttpPost("delete-many")]
    public async Task<ActionResult<List<EmployeeDto>>> DeleteMany(List<IdDto> employees)
    {
        try
        {
            return Ok(await employeeService.DeleteMany(employees));
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}
