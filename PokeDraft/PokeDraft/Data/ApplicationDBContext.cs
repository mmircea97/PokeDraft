using Microsoft.EntityFrameworkCore;
using PokeDraft.Models;
using Type = PokeDraft.Models.Type;

namespace PokeDraft.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { 
        
        }

        public DbSet<Type> Types { get; set; }
        public DbSet<Species> Species { get; set; }
    }
}
