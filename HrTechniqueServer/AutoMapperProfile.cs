using HrTechniqueServer.Dto;
using HrTechniqueServer.Models;

namespace HrTechniqueServer;

// ReSharper disable once UnusedType.Global
public class AutoMapperProfile : ClassLibrary.Dtos.AutoMapperProfile
{
    public AutoMapperProfile()
    {
        CreateMap<EmployeeDto, Employee>().ReverseMap();
    }
}
