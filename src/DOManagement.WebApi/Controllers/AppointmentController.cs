using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DOManagement.Application.Appointments.Commands;
using DOManagement.Application.Appointments.Queries;

namespace DOManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ApiControllerBase
    {

        [HttpPost]
        public async Task<ActionResult> CreateAppointment([FromBody] CreateAppointmentCommand command)
        {
            var appointment = await Mediator.Send(command);
            return CreatedAtAction("GetAppointment", new { id = appointment.Id }, appointment);
        }

        [HttpGet("all")]
        public async Task<ActionResult> GetAllAppointments()
        {
           var query = new GetAllAppointmentsQuery();
           var result = await Mediator.Send(query);
           return Ok(result);
        }

        [HttpGet()]
        public async Task<ActionResult> GetAppointment([FromQuery] GetAppointmentByIdQuery query)
        {
            var result = await Mediator.Send(query);
           return (result != null) ? (ActionResult) Ok(result) : NotFound();
        }

        [HttpPatch]
        public async Task<ActionResult> UpdateAppointment([FromBody] UpdateAppointmentCommand command)
        {
            var appointment = await Mediator.Send(command);
            return Ok(appointment);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAppointment([FromBody] DeleteAppointmentCommand command)
        {
            var appointment = await Mediator.Send(command);
            return Ok(appointment);
        }
    }
}
