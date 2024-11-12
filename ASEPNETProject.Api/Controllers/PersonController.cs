using ASEPNETProject.Data.Models;
using ASEPNETProject.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASEPNETProject.Api.Controllers
{
    [Route("api/people")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _personRepo;
        private readonly ILogger<PersonController> _logger;
        public PersonController(IPersonRepository personRepo, ILogger<PersonController> logger)
        {
            _personRepo = personRepo;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> AddPerson(Person person)
        {
            try
            {
                var createdPerson = await _personRepo.CreatePersonAsync(person);
                return CreatedAtAction(nameof(AddPerson), createdPerson);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = 500,
                    message = ex.Message
                });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePerson(Person personToUpdate)
        {
            try
            {
                var existingPerson = await _personRepo.GetPeopleByIdAsync(personToUpdate.Id);
                if (existingPerson == null)
                {
                    return NotFound(new {
                        StatusCode=404,
                            message="Record Not Found"
                    });
                }
                existingPerson.Name = personToUpdate.Name;
                existingPerson.Email = personToUpdate.Email;
                await _personRepo.CreatePersonAsync(existingPerson);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = 500,
                    message = ex.Message
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            try
            {
                var existingPerson = await _personRepo.GetPeopleByIdAsync(id);
                if (existingPerson == null)
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        message = "Record Not Found"
                    });
                }
                await _personRepo.DeletePersonAsync(existingPerson);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = 500,
                    message = ex.Message
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetPeople()
        {
            try
            {
                var people = await _personRepo.GetPeopleAsync();
                return Ok(people);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = 500,
                    message = ex.Message
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPeopple(int id)
        {
            try
            {
                var person = await _personRepo.GetPeopleByIdAsync(id);
                if (person == null)
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        message = "Record Not Found"
                    });
                }
                return Ok(person);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    StatusCode = 500,
                    message = ex.Message
                });
            }
        }
    }
}
