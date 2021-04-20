using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DOManagement.Application.Common.Models;
using DOManagement.Domain.Entities;
using DOManagement.Infrastructure.Interfaces;
using MediatR;

namespace DOManagement.Application.Patients.Commands
{
    public class CreatePatientAllergyCommand : IRequest<PatientModel>
    {
        public long PatientId { get;set; }
        public long AllergyId { get; set; }
    }
    
    public class CreatePatientAllergyHandler : IRequestHandler<CreatePatientAllergyCommand, PatientModel>
    {
        private readonly IPatientRepository _repository;

        public CreatePatientAllergyHandler(IPatientRepository repository)
        {
            _repository = repository;
        }

        public async Task<PatientModel> Handle(CreatePatientAllergyCommand request, CancellationToken cancellationToken)
        {
            await _repository.CreatePatientAllergyAsync(request.PatientId, new Allergy()
            {
                Id = request.AllergyId
            });
            
            var patient = await _repository.GetPatientByIdAsync(request.PatientId);
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