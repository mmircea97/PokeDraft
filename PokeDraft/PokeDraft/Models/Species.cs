using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PokeDraft.Models
{
    public class Species
    {
        [Key]
        public int SpeciesId { get; set; }
        public string SpeciesName { get; set;} = String.Empty;
        public int? SpeciesEvolutionId {  get; set; }
        public int? EvolutionLevel { get; set; }
        public string PrimaryType { get; set; } = String.Empty;
        public string? SecondaryType { get; set; } = String.Empty;
        public byte HP { get; set; }
        public byte Attack { get; set; }
        public byte Defense { get; set; }
        public byte SpAttack {  get; set; }
        public byte SpDefense { get; set; }
        public byte Speed { get; set; }
        [Required]
        public string ImageName { get; set; } = String.Empty;
    }
}
