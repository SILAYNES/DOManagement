using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DOManagement.Application.Common.Models;
using DOManagement.Infrastructure.Interfaces;
using MediatR;

namespace DOManagement.Application.Appointments.Queries
{
    public class GetAllAppointmentsQuery : IRequest<IEnumerable<AppointmentModel>>
    {
    }

    public class GetAllAppointmentsHandler : IRequestHandler<GetAllAppointmentsQuery, IEnumerable<AppointmentModel>>
    {
        private readonly IAppointmentRepository _repository;

        public GetAllAppointmentsHandler(IAppointmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AppointmentModel>> Handle(GetAllAppointmentsQuery request, CancellationToken cancellationToken)
        {
            var appointments = await _repository.GetAppointmentsAsync();
            return appointments.Select(a => new AppointmentModel()
            {
                Id = a.Id,
                DateAt = a.DateAt,
                Description = a.Description,
                Completed = a.Completed,
                Specialist = new SpecialistModel()
                {
                    Id = a.Specialist.Id,
                    Names = a.Specialist.Names,
                    Surnames = a.Specialist.Surnames
                },
                Patient = new PatientModel()
                {
                    Id = a.Patient.Id,
                    Names = a.Patient.Names,
                    Surnames = a.Patient.Surnames
                }
            });
        }
    }
}