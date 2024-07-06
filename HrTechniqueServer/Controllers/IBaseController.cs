using ClassLibrary.Dtos.SharedDtos;
using Microsoft.AspNetCore.Mvc;

namespace HrTechniqueServer.Controllers;

public interface IBaseController<T>
    where T : class
{
    Task<ActionResult<T>> Create(T obj);
    Task<ActionResult<T>> Get(int id);
    Task<ActionResult<List<T>>> GetAll();
    Task<ActionResult<T>> Update(T obj);
    Task<ActionResult<List<T>>> DeleteMany(List<IdDto> obj);
}
