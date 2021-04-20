using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DOManagement.Application.Common.Models;
using DOManagement.Infrastructure.Interfaces;
using MediatR;

namespace DOManagement.Application.Patients.Commands
{
    public class DeletePatientCommand : IRequest<PatientModel>
    {
        public long PatientId { get; set; }
    }
    
    public class DeletePatientHandler : IRequestHandler<DeletePatientCommand, PatientModel>
    {
        private readonly IPatientRepository _repository;

        public DeletePatientHandler(IPatientRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<PatientModel> Handle(DeletePatientCommand request, CancellationToken cancellationToken)
        {
            var patient = await _repository.GetPatientByIdAsync(request.PatientId);
            patient.Enabled = false;
            patient.LastModified = DateTime.UtcNow;
            patient.LastModifiedBy = "APPLICATION";
            
            long resultId = await _repository.UpdatePatientAsync(patient);
            patient = await _repository.GetPatientByIdAsync(resultId);
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