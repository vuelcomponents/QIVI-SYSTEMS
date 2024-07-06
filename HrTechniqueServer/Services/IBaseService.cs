using ClassLibrary.Dtos;
using ClassLibrary.Dtos.Auth;
using ClassLibrary.Dtos.SharedDtos;

namespace HrTechniqueServer.Services;

public interface IBaseService<T>
    where T : class
{
    Task<T> Get(int id);
    Task<List<T>> Get();
    Task<T> Create(UserShortDto user, T obj);
    Task<T> Update(UserShortDto user, T obj);
    Task<List<T>> DeleteMany(List<IdDto> list);
}
