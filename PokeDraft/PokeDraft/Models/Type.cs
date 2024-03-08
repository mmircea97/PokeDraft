using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace PokeDraft.Models
{
    public class Type
    {
        [Key]
        public string TypeName { get; set; } = String.Empty;
    }
}
