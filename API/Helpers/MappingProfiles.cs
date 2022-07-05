using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<TVShow, TVShowToReturnDto>()
            .ForMember(d => d.TVShowGenre, o => o.MapFrom(s => s.TVShowGenre.Name));
    }
}
