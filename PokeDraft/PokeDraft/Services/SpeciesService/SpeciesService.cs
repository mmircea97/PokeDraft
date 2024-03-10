using Microsoft.EntityFrameworkCore;
using PokeDraft.Data;
using PokeDraft.DTOs;
using PokeDraft.Exceptions.Species;
using PokeDraft.Models;
using PokeDraft.Services.TypesServices;
using PokeDraft.Helpers;
using AutoMapper;
using PokeDraft.Exceptions.Type;
using PokeDraft.Helpers.Validators;
using FluentValidation;
using Type = PokeDraft.Models.Type;

namespace PokeDraft.Services.SpeciesService
{
    public class SpeciesService : ISpeciesService
    {
        private readonly ApplicationDBContext _context;
        private readonly ITypesService _typesService;
        private readonly IMapper _mapper;
        private readonly IValidator<Species> _validator;
        private const string _elementName = "species";

        public SpeciesService(ApplicationDBContext context, ITypesService typesService, IMapper mapper, IValidator<Species> validator)
        {
            _context = context;
            _typesService = typesService;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<IEnumerable<Species>> GetSpeciesAsync()
        {
            return await _context.Species.ToListAsync();
        }


        public async Task<Species?> GetSpeciesByIdAsync(int id)
        {
            var species = await _context.Species.FindAsync(id);
            if (species == null)
            {
                throw new SpeciesDoesNotExistException(String.Format(FailureMessages.ElementNotFound, _elementName));
            }
            return species;
        }

        public async Task<Species?> GetSpeciesByName(string speciesName)
        {
            var species = await _context.Species.FirstOrDefaultAsync(a=>a.SpeciesName == speciesName);
            if(species == null)
            {
                throw new SpeciesDoesNotExistException(String.Format(FailureMessages.ElementNotFound, _elementName));
            }
            return species;
        }

        //CREATE SPECIES
        public async Task<bool> CreateSpeciesAsync(CreateSpeciesDTO species)
        {
            if(await NameExists(species.SpeciesName))
            {
                throw new SpeciesAlreadyExists(String.Format(FailureMessages.ElementAlreadyExists, _elementName, species.SpeciesName));
            }

            if (! await _typesService.NameExists(species.PrimaryType))
            {
                throw new TypeDoesNotExistException(String.Format(FailureMessages.SpeciesWrongType, species.PrimaryType));
            }

            if (species.SecondaryType != null)
            {
                if (! await _typesService.NameExists(species.SecondaryType))
                {
                    throw new TypeDoesNotExistException(String.Format(FailureMessages.SpeciesWrongType, species.SecondaryType));
                }
            }

            var newSpecies = _mapper.Map<Species>(species);

            var validationResult = await _validator.ValidateAsync(newSpecies);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(FailureMessages.InvalidInput + " " + validationResult.ToString());
            }

            _context.Species.Add(newSpecies);
            await _context.SaveChangesAsync();
            return true;
        }


        //Update the two fields of the model that could not be added at creation
        public async Task<Species?> AddEvolutionData(int id, AddSpeciesEvolutionDataDTO evolutionData)
        {
            var species = await GetSpeciesByIdAsync(id);
            if (species == null)
            {
                throw new SpeciesDoesNotExistException(String.Format(FailureMessages.ElementNotFound, _elementName));
            }
            if(evolutionData.SpeciesEvolutionId != null)
            {
                if (await GetSpeciesByIdAsync((int)evolutionData.SpeciesEvolutionId) == null)
                {
                    throw new SpeciesDoesNotExistException(String.Format(FailureMessages.SpeciesEvolutionDoesNotExist, evolutionData.SpeciesEvolutionId));
                }
            }


            species.SpeciesEvolutionId = evolutionData.SpeciesEvolutionId;
            species.EvolutionLevel = evolutionData.EvolutionLevel;

            var validationResult = await _validator.ValidateAsync(species);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(FailureMessages.InvalidInput + " " + validationResult.ToString());
            }

            _context.Species.Update(species);
            await _context.SaveChangesAsync();
            return species;
        }

        //Updates the typing for a species
        public async Task<Species?> ModifyTyping(int id, ModifyTypingDTO typing)
        {
            var species = await GetSpeciesByIdAsync(id);
            if (species == null)
            {
                throw new SpeciesDoesNotExistException(String.Format(FailureMessages.ElementNotFound, _elementName));
            }
            if (!await _typesService.NameExists(typing.PrimaryType))
            {
                throw new TypeDoesNotExistException(String.Format(FailureMessages.SpeciesWrongType, species.PrimaryType));
            }

            if (typing.SecondaryType != null)
            {
                if (!await _typesService.NameExists(typing.SecondaryType))
                {
                    throw new TypeDoesNotExistException(String.Format(FailureMessages.SpeciesWrongType, species.SecondaryType));
                }
            }
            species.PrimaryType = typing.PrimaryType;
            species.SecondaryType = typing.SecondaryType;

            var validationResult = await _validator.ValidateAsync(species);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(FailureMessages.InvalidInput + " " + validationResult.ToString());
            }

            _context.Species.Update(species);
            await _context.SaveChangesAsync();
            return species;

        }


        public async Task<Species?> ModifyImageName(int id, ModifyImageNameDTO imageName)
        {
            var species = await GetSpeciesByIdAsync(id);
            if (species == null)
            {
                throw new SpeciesDoesNotExistException(String.Format(FailureMessages.ElementNotFound, _elementName));
            }
            species.ImageName = imageName.ImageName;

            var validationResult = await _validator.ValidateAsync(species);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(FailureMessages.InvalidInput + " " + validationResult.ToString());
            }

            _context.Species.Update(species);
            await _context.SaveChangesAsync();
            return species;
        }


        public async Task<bool> DeleteSpeciesByNameAsync(DeleteSpeciesByNameDTO speciesName)
        {
            var species = await GetSpeciesByName(speciesName.SpeciesName);
            if (species == null)
            {
                throw new SpeciesDoesNotExistException(String.Format(FailureMessages.ElementNotFound, _elementName));
            }

            _context.Species.Remove(species);
            await _context.SaveChangesAsync();
            return true;
        }

        //Checks if a species with the given name already exists.
        public async Task<bool> NameExists(String speciesName)
        {
            return await _context.Species.AnyAsync(a => a.SpeciesName == speciesName);
        }
    }
}
