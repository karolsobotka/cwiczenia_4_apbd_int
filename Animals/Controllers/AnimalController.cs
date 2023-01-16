using Animals.Exceptions;
using Animals.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Animals.Controllers
{
    [Route("api/animals")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly IDBService _dbService;

        public AnimalsController(IDBService dbService)
        {
            this._dbService = dbService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAnimals([FromQuery] string orderBy)
        {
            List<Animal> animals = null;
            try { animals = _dbService.GetAnimals(orderBy); }
            catch (DBNoRowsException) { return NotFound("There is no rows in database!"); }
            catch (NotMatchedColumnNameException) { return BadRequest("You can order only by name, description, category or area!"); }
            return Ok(animals);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnimal([FromBody] Animal animal)
        {
            try { _dbService.CreateAnimal(animal); }
            catch (Exception) { return BadRequest("Entered data is not valid!"); }
            return Ok("Succsesfully created!");
        }

        [HttpPut("{idAnimal}")]
        public async Task<IActionResult> ChangeAnimal([FromRoute] int idAnimal, [FromBody] Animal animal)
        {
            try { _dbService.ChangeAnimal(idAnimal, animal); }
            catch (NoExecutedQueryException) { return NotFound($"No such animal found with ID {idAnimal}!"); }
            catch (Exception) { return BadRequest("Entered data is not valid!"); }
            return Ok("Succsesfully changed!");
        }

        [HttpDelete("{idAnimal}")]
        public async Task<IActionResult> DeleteAnimal([FromRoute] int idAnimal)
        {
            try { _dbService.DeleteAnimal(idAnimal); }
            catch (NoExecutedQueryException) { return NotFound($"No such animal found with ID {idAnimal}!"); }
            catch (Exception) { return BadRequest("Entered data is not valid!"); }
            return Ok("Succsesfully deleted!");
        }


    }
}