using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokeDraft.DTOs;
using PokeDraft.Exceptions.Species;
using PokeDraft.Exceptions.Type;
using PokeDraft.Services.SpeciesService;
using System.Drawing;
using System.Net;

namespace PokeDraft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpeciesController : ControllerBase
    {
        private readonly ISpeciesService _speciesService;

        public SpeciesController(ISpeciesService speciesService)
        {
            _speciesService = speciesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSpecies()
        {
            var species = await _speciesService.GetSpeciesAsync();
            return Ok(species);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSpeciesById([FromRoute]int id)
        {
            try
            {
                var species = await _speciesService.GetSpeciesByIdAsync(id);
                return Ok(species);
            }
            catch (SpeciesDoesNotExistException ex) {
                return StatusCode((int)HttpStatusCode.NotFound, ex.Message);
            }
        }

        [HttpGet("{speciesName}")]
        public async Task<IActionResult> GetSpeciesByName([FromRoute]string speciesName)
        {
            try
            {
                var species = await _speciesService.GetSpeciesByName(speciesName);
                return Ok(species);
            }
            catch (SpeciesDoesNotExistException ex)
            {
                return StatusCode((int)HttpStatusCode.NotFound, ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> PostSpecies([FromBody]CreateSpeciesDTO species)
        {
            try
            {
                await _speciesService.CreateSpeciesAsync(species);
                return Ok(species);
            }
            catch (SpeciesAlreadyExists ex)
            {
                return StatusCode((int)HttpStatusCode.Conflict, ex.Message);
            }
            catch (TypeDoesNotExistException ex)
            {
                return StatusCode((int)HttpStatusCode.NotFound, ex.Message);
            }
        }


        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateSpeciesEvolutionData([FromRoute]int id, [FromBody]AddSpeciesEvolutionDataDTO evolutionData)
        {
            try
            {
                var updatedSpecies = await _speciesService.AddEvolutionData(id, evolutionData);
                return Ok(updatedSpecies);
            }
            catch (SpeciesDoesNotExistException ex)
            {
                return StatusCode((int)HttpStatusCode.NotFound, ex.Message);
            }
        }

        [HttpPatch("type/{id}")]
        public async Task<IActionResult> UpdateSpeciesTyping([FromRoute]int id, [FromBody]ModifyTypingDTO typing)
        {
            try
            {
                var updatedSpecies = await _speciesService.ModifyTyping(id, typing);
                return Ok(updatedSpecies);
            }
            catch (SpeciesDoesNotExistException ex)
            {
                return NotFound(ex.Message);
            }
            catch (TypeDoesNotExistException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
