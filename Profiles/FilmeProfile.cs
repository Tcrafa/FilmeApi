using AutoMapper;
using FilmeApi.Data.Dtos;
using FilmeApi.Models;

namespace FilmeApi.Profiles;

public class FilmeProfile: Profile
{
    public FilmeProfile()
    {
        CreateMap<FilmeCreateDto, Filme>();
        CreateMap<FilmeReadDto, Filme>();
        CreateMap<Filme, FilmeUpdateDto>();
        CreateMap<Filme, FilmeReadDto>()
            .ForMember(filmeDto => filmeDto.Sessoes, 
            opt => opt.MapFrom(filme => filme.Sessoes));
    }
}
