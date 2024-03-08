using PokeDraft.DTOs;
using PokeDraft.Models;

namespace PokeDraft.Services.SpeciesService
{
    public interface ISpeciesService
    {
        public Task<IEnumerable<Species>> GetSpeciesAsync();
        public Task<bool> CreateSpeciesAsync(CreateSpeciesDTO species);
        public Task<Species?> GetSpeciesByIdAsync(int id);
        public Task<Species?> AddEvolutionData(int id, AddSpeciesEvolutionDataDTO evolutionData);
        public Task<Species?> ModifyTyping(int id, ModifyTypingDTO typing);
        public Task<Species?> GetSpeciesByName(string speciesName);
    }
}
