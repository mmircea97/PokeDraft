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
        public TypeController(ITypesService typesService)
        {
            _typesService = typesService;
        }



        [HttpGet]
        public async Task<IActionResult> GetTypes()
        {
            var types = await _typesService.GetTypesAsync();
            return Ok(types);
        }



        [HttpGet("{typeName}")]
        public async Task<IActionResult> GetTypeByName([FromRoute] string typeName)
        {
            var type = await _typesService.GetTypeByIdAsync(typeName);

            if (type == null)
            {
                return StatusCode((int)HttpStatusCode.NotFound);
            }
            return Ok(type);
        }


        [HttpPost]
        public async Task<IActionResult> PostType([FromBody] Type type)
        {
            try
            {
                await _typesService.CreateTypeAsync(type);
                return Ok(type);
            }
            catch (TypeAlreadyExistsException ex)
            {
                return StatusCode((int)HttpStatusCode.Conflict, ex.Message);
            }

        }


        [HttpDelete]
        public async Task<IActionResult> DeleteType([FromBody] Type type)
        {
            try
            {
                await _typesService.DeleteTypeAsync(type);
                return Ok(type);
            }
            catch (TypeDoesNotExistException ex)
            {
                return StatusCode((int)HttpStatusCode.NotFound, ex.Message);
            }
        }


        [HttpPut]
        public async Task<IActionResult> UpdateType([FromBody] Type type, [FromQuery] string typeName)
        {
            try
            {
                await _typesService.UpdateTypeAsync(type, typeName);
                return Ok(type);
            }
            catch (TypeDoesNotExistException ex)
            {
                return StatusCode((int)HttpStatusCode.NotFound, ex.Message);
            }
        }
    }
}
