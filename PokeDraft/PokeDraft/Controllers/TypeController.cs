using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokeDraft.Exceptions.Type;
using PokeDraft.Services.TypesServices;
using System.Net;
using Type = PokeDraft.Models.Type;

namespace PokeDraft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypeController : ControllerBase
    {

        private readonly ITypesService _typesService;
        private readonly ILogger<TypeController> _logger;
        public TypeController(ITypesService typesService, ILogger<TypeController> logger)
        {
            _typesService = typesService;
            _logger = logger;
        }



        [HttpGet]
        public async Task<IActionResult> GetTypes()
        {
            _logger.LogInformation("GetTypes START");
            var types = await _typesService.GetTypesAsync();
            _logger.LogInformation($"GetTypes END, total result: {types.Count()}");
            return Ok(types);
        }



        [HttpGet("{typeName}")]
        public async Task<IActionResult> GetTypeByName([FromRoute] string typeName)
        {
            _logger.LogInformation($"GetTypeByName START, looking for {typeName}");
            var type = await _typesService.GetTypeByIdAsync(typeName);

            if (type == null)
            {
                _logger.LogInformation($"{typeName} not found, returning status code NotFound. GetTypeByName END.");
                return StatusCode((int)HttpStatusCode.NotFound);
            }
            _logger.LogInformation($"{typeName} found, returning status code 200. GetTypeByName END.");
            return Ok(type);
        }


        [HttpPost]
        public async Task<IActionResult> PostType([FromBody] Type type)
        {
            _logger.LogInformation("PostType START.");
            try
            {
                _logger.LogInformation("PostType START.");
                await _typesService.CreateTypeAsync(type);
                _logger.LogInformation($"{type.TypeName} added to database. PostType END.");
                return Ok(type);
            }
            catch (TypeAlreadyExistsException ex)
            {
                _logger.LogInformation($"{type.TypeName} already exists in the database. PostType END with exception: {ex.Message}");
                return StatusCode((int)HttpStatusCode.Conflict, ex.Message);
            }
            catch (ValidationException ex)
            {
                _logger.LogInformation($"{type.TypeName} is not valid. PostType END with exception: {ex.Message}");
                return BadRequest(ex.Message);
            }

        }


        [HttpDelete]
        public async Task<IActionResult> DeleteType([FromBody] Type type)
        {
            try
            {
                _logger.LogInformation("DeleteType START.");
                await _typesService.DeleteTypeAsync(type);
                _logger.LogInformation($"{type.TypeName} deleted from database sucessfully. DeleteType END.");
                return Ok(type);
            }
            catch (TypeDoesNotExistException ex)
            {
                _logger.LogInformation($"{type.TypeName} did not exist in the database. DeleteType END with message: {ex.Message}");
                return StatusCode((int)HttpStatusCode.NotFound, ex.Message);
            }
            catch (ValidationException ex)
            {
                _logger.LogInformation($"{type.TypeName} is not valid. DeleteType END with message: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }


        [HttpPut]
        public async Task<IActionResult> UpdateType([FromBody] Type type, [FromQuery] string typeName)
        {
            try
            {
                _logger.LogInformation("UpdateType START.");
                await _typesService.UpdateTypeAsync(type, typeName);
                _logger.LogInformation($"{typeName} updated successfully. UpdateType END.");
                return Ok(type);
            }
            catch (TypeDoesNotExistException ex)
            {
                _logger.LogInformation($"{type.TypeName} does not exist in the database. Value could not be updated with {typeName}. UpdateType END.");
                return StatusCode((int)HttpStatusCode.NotFound, ex.Message);
            }
            catch (ValidationException ex)
            {
                _logger.LogInformation($"Either {type.TypeName} or {typeName} are invalid. UpdateType END with message: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}
