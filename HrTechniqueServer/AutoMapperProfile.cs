using HrTechniqueServer.Domain.Entities;
using HrTechniqueServer.Dto;

namespace HrTechniqueServer;

// ReSharper disable once UnusedType.Global
public class AutoMapperProfile : ClassLibrary.Dtos.AutoMapperProfile
{
    public AutoMapperProfile()
    {
        CreateMap<EmployeeDto, Employee>().ReverseMap();
    }
}
