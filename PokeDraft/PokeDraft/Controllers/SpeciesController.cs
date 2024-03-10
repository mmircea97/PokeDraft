using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokeDraft.DTOs;
using PokeDraft.Exceptions.Species;
using PokeDraft.Exceptions.Type;
using PokeDraft.Helpers;
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
        private readonly ILogger<SpeciesController> _logger;

        public SpeciesController(ISpeciesService speciesService, ILogger<SpeciesController> logger)
        {
            _speciesService = speciesService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetSpecies()
        {
            _logger.LogInformation("GetSpecies START.");
            var species = await _speciesService.GetSpeciesAsync();
            _logger.LogInformation("GetSpecies END.");
            return Ok(species);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSpeciesById([FromRoute]int id)
        {
            try
            {
                _logger.LogInformation("GetSpeciesById START.");
                var species = await _speciesService.GetSpeciesByIdAsync(id);
                _logger.LogInformation("Species found successfully. GetSpeciesById END");
                return Ok(species);
            }
            catch (SpeciesDoesNotExistException ex) {
                _logger.LogInformation($"Species with the id: {id} does not exist. GetSpeciesById END with message: {ex.Message}");
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
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
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
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
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
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("img/{id}")]
        public async Task<IActionResult> UpdateSpeciesImage([FromRoute]int id, [FromBody]ModifyImageNameDTO imageName)
        {
            try
            {
                var updatedSpecies = await _speciesService.ModifyImageName(id, imageName);
                return Ok(updatedSpecies);
            }
            catch (SpeciesDoesNotExistException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSpeciesByName([FromBody]DeleteSpeciesByNameDTO speciesName)
        {
            try
            {
                await _speciesService.DeleteSpeciesByNameAsync(speciesName);
                return Ok(SuccessMessages.ElementDeletedSuccessfully);
            }
            catch (SpeciesDoesNotExistException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
