using System.Threading.Tasks;
using DOManagement.Application.Specialists.Commands;
using DOManagement.Application.Specialists.Queries;
using Microsoft.AspNetCore.Mvc;

namespace DOManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialistController : ApiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> CreateSpecialist([FromBody] CreateSpecialistCommand command)
        {
            var patient = await Mediator.Send(command);
            return CreatedAtAction("GetSpecialist", new { id = patient.Id }, patient);
        }


        [HttpGet("all")]
        public async Task<ActionResult> GetAllSpecialists()
        {
           var query = new GetAllSpecialistQuery();
           var result = await Mediator.Send(query);
           return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult> GetSpecialist([FromQuery] GetSpecialistByIdQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPatch]
        public async Task<ActionResult> UpdateSpecialist([FromBody] UpdateSpecialistCommand command)
        {
            var patient = await Mediator.Send(command);
            return Ok(patient);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteSpecialist([FromBody] DeleteSpecialistCommand command)
        {
            var patient = await Mediator.Send(command);
            return Ok(patient);
        }
     
    }
}