using ClassLibrary.Dtos.SharedDtos;
using Microsoft.AspNetCore.Mvc;

namespace HrTechniqueServer.Web.Controllers;

public interface IBaseController<T>
    where T : class
{
    Task<ActionResult<T>> Create(T obj);
    Task<ActionResult<T>> Get(int id);
    Task<ActionResult<List<T>>> GetAll();
    Task<ActionResult<T>> Update(T obj);
    Task<IActionResult> DeleteMany(List<IdDto> obj);
}
