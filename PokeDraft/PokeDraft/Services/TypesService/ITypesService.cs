using Type = PokeDraft.Models.Type;

namespace PokeDraft.Services.TypesServices
{
    public interface ITypesService
    {
        public Task<IEnumerable<Type>> GetTypesAsync();
        public Task<Type?> GetTypeByIdAsync(string typeName);
        public Task<bool> CreateTypeAsync(Type type);
        public Task<bool> DeleteTypeAsync(Type type);
        public Task<Type?> UpdateTypeAsync(Type type, string typeName);
    }
}
