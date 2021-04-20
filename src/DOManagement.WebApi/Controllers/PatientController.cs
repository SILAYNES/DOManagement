using System.Threading.Tasks;
using DOManagement.Application.Patients.Commands;
using DOManagement.Application.Patients.Queries;
using Microsoft.AspNetCore.Mvc;

namespace DOManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ApiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> CreatePatient([FromBody] CreatePatientCommand command)
        {
            var patient = await Mediator.Send(command);
            return CreatedAtAction("GetPatient", new { id = patient.Id }, patient);
        }

        [HttpPost("{id}/allergies")] // TODO: fix fromPath and fromBody patient allergy insert
        public async Task<ActionResult> CreatePatientAllergy([FromBody] CreatePatientAllergyCommand command)
        {
            var patient = await Mediator.Send(command);
            return CreatedAtAction("GetPatient", new { id = patient.Id }, patient);
        }

        [HttpGet("all")]
        public async Task<ActionResult> GetAllPatients()
        {
            var query = new GetAllPatientsQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult> GetPatient([FromQuery] GetPatientByIdQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPatch]
        public async Task<ActionResult> UpdatePatient([FromBody] UpdatePatientCommand command)
        {
            var patient = await Mediator.Send(command);
            return Ok(patient);
        }

        [HttpDelete]
        public async Task<ActionResult> DeletePatient([FromBody] DeletePatientCommand command)
        {
            var patient = await Mediator.Send(command);
            return Ok(patient);
        }

        [HttpDelete("allergies")] // TODO: fix fromPath and fromBody patient allergy delete
        public async Task<ActionResult> DeletePatientAllergy([FromQuery] DeletePatientAllergyCommand command)
        {
            var patient = await Mediator.Send(command);
            return Ok(patient);
        }
    }
}