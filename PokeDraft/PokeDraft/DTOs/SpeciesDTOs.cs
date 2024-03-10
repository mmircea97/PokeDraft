using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace PokeDraft.DTOs;


//Create will ignore the evolution data, as the id of the species it evolves into may not yet exist in the table.
public record CreateSpeciesDTO(
    string SpeciesName,
    string PrimaryType,
    string? SecondaryType,
    byte HP,
    byte Attack,
    byte Defense,
    byte SpAttack,
    byte SpDefense,
    byte Speed,
    string ImageName
);

public record AddSpeciesEvolutionDataDTO(
    int? SpeciesEvolutionId,
    int? EvolutionLevel
);

public record ModifyTypingDTO(
    string PrimaryType,
    string? SecondaryType
);

public record ModifyImageNameDTO(
    string ImageName
);

public record DeleteSpeciesByNameDTO(
    string SpeciesName
);