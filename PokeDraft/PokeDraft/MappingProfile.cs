using AutoMapper;
using PokeDraft.DTOs;
using PokeDraft.Models;

namespace PokeDraft
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<Species, CreateSpeciesDTO>().ReverseMap();
        }
    }
}
