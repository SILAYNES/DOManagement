using System;
using System.Threading;
using System.Threading.Tasks;
using DOManagement.Application.Common.Models;
using DOManagement.Domain.Entities;
using DOManagement.Infrastructure.Interfaces;
using MediatR;

namespace DOManagement.Application.Appointments.Commands
{
    public class UpdateAppointmentCommand : IRequest<AppointmentModel>
    {
        public long Id { get; set; }
        public DateTime DateAt { get; set; }
        public string Description { get; set; }
        public bool? Completed { get; set; }
        public long SpecialistId { get; set; }
        public long PatientId { get; set; }
    }
    
    public class UpdateAppointmentHandler : IRequestHandler<UpdateAppointmentCommand, AppointmentModel>
    {
        private readonly IAppointmentRepository _repository;

        public UpdateAppointmentHandler(IAppointmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<AppointmentModel> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
        {
            long insertedId = await _repository.CreateAppointmentAsync(new Appointment()
            {
                DateAt = request.DateAt,
                Description = request.Description,
                Completed = false,
                Specialist = new Specialist() { Id = request.SpecialistId },
                Patient = new Patient() { Id = request.PatientId },
                CreatedBy = "APPLICATION",
                LastModified = DateTime.UtcNow,
                LastModifiedBy = "APPLICATION"
            });
            
            var appointment = await _repository.GetAppointmentByIdAsync(insertedId);
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