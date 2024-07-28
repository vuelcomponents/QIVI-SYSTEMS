using ClassLibrary.Dtos.SharedDtos;
using HrTechniqueServer.Shared.Resources;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HrTechniqueServer.Web.Controllers;

public abstract class BaseCrudController<
    TDto,
    TCreateCommand,
    TUpdateCommand,
    TDeleteCommand,
    TGetQuery,
    TGetAllQuery
>(IMediator mediator, IAuthResource authResource) : ControllerBase
    where TDto : class
    where TCreateCommand : IRequest<TDto>
    where TUpdateCommand : IRequest<TDto>
    where TDeleteCommand : IRequest
    where TGetQuery : IRequest<TDto>
    where TGetAllQuery : IRequest<List<TDto>>
{
    [HttpGet("get/{id}")]
    public async Task<ActionResult<TDto>> Get(int id)
    {
        try
        {
            var query = (TGetQuery)Activator.CreateInstance(typeof(TGetQuery), id)!;
            var result = await mediator.Send(query);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("get")]
    public async Task<ActionResult<List<TDto>>> GetAll()
    {
        try
        {
            var query = (TGetAllQuery)Activator.CreateInstance(typeof(TGetAllQuery))!;
            var result = await mediator.Send(query);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("create")]
    public async Task<ActionResult<TDto>> Create([FromBody] TDto dto)
    {
        try
        {
            var command = (TCreateCommand)
                Activator.CreateInstance(
                    typeof(TCreateCommand),
                    dto,
                    authResource.User!
                )!;
            var result = await mediator.Send(command);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPatch("update")]
    public async Task<ActionResult<TDto>> Update([FromBody] TDto dto)
    {
        try
        {
            var command = (TUpdateCommand)Activator.CreateInstance(typeof(TUpdateCommand), dto)!;
            var result = await mediator.Send(command);
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("delete-many")]
    public async Task<IActionResult> DeleteMany([FromBody] List<IdDto> ids)
    {
        try
        {
            var command = (TDeleteCommand)Activator.CreateInstance(typeof(TDeleteCommand), ids)!;
            await mediator.Send(command);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
