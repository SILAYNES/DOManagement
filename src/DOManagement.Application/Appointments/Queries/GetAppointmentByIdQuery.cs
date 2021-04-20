using System.Threading;
using System.Threading.Tasks;
using DOManagement.Application.Common.Models;
using DOManagement.Infrastructure.Interfaces;
using MediatR;

namespace DOManagement.Application.Appointments.Queries
{
    public class GetAppointmentByIdQuery : IRequest<AppointmentModel>
    {
        public long Id { get; set; }        
    }

    public class GetAppointmentByIdHandler : IRequestHandler<GetAppointmentByIdQuery, AppointmentModel>
    {
        private readonly IAppointmentRepository _repository;

        public GetAppointmentByIdHandler(IAppointmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<AppointmentModel> Handle(GetAppointmentByIdQuery request, CancellationToken cancellationToken)
        {
            var appointment = await _repository.GetAppointmentByIdAsync(request.Id);
            return new AppointmentModel()
            {
                Id = appointment.Id,
                DateAt = appointment.DateAt,
                Description = appointment.Description,
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