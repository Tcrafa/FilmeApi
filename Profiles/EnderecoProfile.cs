using AutoMapper;
using FilmeApi.Data.Dtos;
using FilmeApi.Models;

namespace FilmeApi.Profiles;

public class EnderecoProfile : Profile
{
    public EnderecoProfile()
    {
        CreateMap<EnderecoCreateDto, Endereco>();
        CreateMap<Endereco, EnderecoReadDto>();
        CreateMap<EnderecoUpdateDto, Endereco>();
    }
}
