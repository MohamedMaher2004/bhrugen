using AutoMapper;
using bhrugen_webapi.Models;
using bhrugen_webapi.Models.Dtos;

namespace bhrugen_webapi;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<Villa, VillaDTO>();
        CreateMap<VillaDTO, Villa>();
        CreateMap<Villa, VillaCreateDTO>().ReverseMap();
        CreateMap<Villa, VillaUpdateDTO>().ReverseMap();
        CreateMap<Villa, VillaUpdateNameDTO>().ReverseMap();
    }
}