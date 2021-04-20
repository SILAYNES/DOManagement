using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DOManagement.Application.Common.Models;
using DOManagement.Domain.Entities;
using DOManagement.Infrastructure.Interfaces;
using MediatR;

namespace DOManagement.Application.Patients.Commands
{
    public class UpdatePatientCommand : IRequest<PatientModel>
    {
        public long Id { get; set; }   
        public string Names { get; set; }
        public string Surnames { get; set; }
        public long Age { get; set; }
        public DateTime Birthday { get; set; }
    }
    
    public class UpdatePatientHandler : IRequestHandler<UpdatePatientCommand, PatientModel>
    {
        private readonly IPatientRepository _repository;

        public UpdatePatientHandler(IPatientRepository repository)
        {
            _repository = repository;
        }

        public async Task<PatientModel> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
        {
            var patient = await _repository.GetPatientByIdAsync(request.Id);
            patient.Names = request.Names;
            patient.Surnames = request.Surnames;
            patient.Age = request.Age;
            patient.Birthday = request.Birthday;
            patient.LastModified = DateTime.UtcNow;
            patient.LastModifiedBy = "APPLICATION";
            
            long insertedId = await _repository.UpdatePatientAsync(patient);
            return new PatientModel()
            {
                Id = patient.Id,
                Names = patient.Names,
                Surnames = patient.Surnames,
                Age = patient.Age,
                Birthday = patient.Birthday,
                Allergies = patient.Allergies.Select(a => new AllergyModel()
                {
                    Id = a.Id,
                    Name = a.Name
                })
            };
        }
    }
}