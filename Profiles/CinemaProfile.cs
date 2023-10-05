using AutoMapper;
using FilmeApi.Data.Dtos;
using FilmeApi.Models;

namespace FilmeApi.Profiles;

public class CinemaProfile : Profile
{
    public CinemaProfile()
    {
        CreateMap<CinemaCreateDto, Cinema>();
        CreateMap<Cinema, CinemaReadDto>()
            .ForMember(cinemaDto => cinemaDto.Endereco,
            opt => opt.MapFrom(cinema => cinema.Endereco))
            .ForMember(cinemaDto => cinemaDto.Sessoes,
            opt => opt.MapFrom(cinema => cinema.Sessoes));
        CreateMap<CinemaUpdateDto, Cinema>();
    }
}
