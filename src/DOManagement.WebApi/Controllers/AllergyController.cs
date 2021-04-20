using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DOManagement.Application.Allergies.Commands;
using DOManagement.Application.Allergies.Queries;

namespace DOManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllergyController : ApiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> CreateAllergy([FromBody] CreateAllergyCommand command)
        {
            var allergy = await Mediator.Send(command);
            return CreatedAtAction("GetAllergy", new { id = allergy.Id }, allergy);
        }

        [HttpGet("all")]
        public async Task<ActionResult> GetAllergies()
        { 
            var result = await Mediator.Send(new GetAllAllergiesQuery()); 
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult> GetAllergy([FromQuery] GetAllergyByIdHandler query)
        {
            var result = await Mediator.Send(query);
            return (result != null) ? (ActionResult) Ok(result) : NotFound();
        }

        [HttpPatch]
        public async Task<ActionResult> UpdateAllergy([FromBody] UpdateAllergyCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAllergy([FromBody] DeleteAllergyCommand command)
        {
            var result = await Mediator.Send(command);
            return Ok(result);
        }
    }
}
