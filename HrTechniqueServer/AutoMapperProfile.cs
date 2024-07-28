using HrTechniqueServer.Domain.Entities;
using HrTechniqueServer.Domain.Dto;

namespace HrTechniqueServer;

// ReSharper disable once UnusedType.Global
public class AutoMapperProfile : ClassLibrary.Dtos.AutoMapperProfile
{
    public AutoMapperProfile()
    {
        CreateMap<EmployeeDto, Employee>().ReverseMap();
    }
}
