using System.Threading;
using System.Threading.Tasks;
using DOManagement.Application.Common.Models;
using DOManagement.Infrastructure.Interfaces;
using MediatR;

namespace DOManagement.Application.Appointments.Commands
{
    public class DeleteAppointmentCommand : IRequest<AppointmentModel>
    {
        public long AppointmentId { get; set; }
    }
    
    public class DeleteAppointmentHandler : IRequestHandler<DeleteAppointmentCommand, AppointmentModel>
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public DeleteAppointmentHandler(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }
        
        public async Task<AppointmentModel> Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
        {
            var resultId = await _appointmentRepository.DeleteAppointmentAsync(request.AppointmentId);
            var appointment = await _appointmentRepository.GetAppointmentByIdAsync(resultId);
            return new AppointmentModel()
            {
                Id = appointment.Id,
                DateAt = appointment.DateAt,
                Completed = appointment.Completed,
                Specialist = new SpecialistModel()
                {
                    Id = appointment.Specialist.Id,
                    Names = appointment.Specialist.Names,
                    Surnames = appointment.Specialist.Surnames
                },
                Patient = new PatientModel()
                {
                    Id = appointment.Patient.Id,
                    Names = appointment.Patient.Names,
                    Surnames = appointment.Patient.Surnames
                }
            };
        }
    }
}