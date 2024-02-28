using System.ComponentModel.DataAnnotations;

namespace PokeDraft.Models
{
    public class Type
    {
        [Key]
        [StringLength(10, ErrorMessage = "Type name must be between 3 and 10 characters", MinimumLength = 3)]
        public string TypeName { get; set; } = String.Empty;
    }
}
