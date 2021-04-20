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
    public class CreatePatientCommand : IRequest<PatientModel>
    {
        public string Names { get; set; }
        public string Surnames { get; set; }
        public long Age { get; set; }
        public DateTime Birthday { get; set; }
    }
    
    public class CreatePatientHandler : IRequestHandler<CreatePatientCommand, PatientModel>
    {
        private readonly IPatientRepository _repository;

        public CreatePatientHandler(IPatientRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<PatientModel> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
        {
            long resultId = await _repository.CreatePatientAsync(new Patient()
            {
                Names = request.Names,
                Surnames = request.Surnames,
                Age = request.Age,
                Birthday = request.Birthday,
                CreatedBy = "APPLICATION"
            });
            
            var patient = await _repository.GetPatientByIdAsync(resultId);
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