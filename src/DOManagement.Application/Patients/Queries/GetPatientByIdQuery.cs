using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DOManagement.Application.Common.Models;
using DOManagement.Infrastructure.Interfaces;
using MediatR;

namespace DOManagement.Application.Patients.Queries
{
    public class GetPatientByIdQuery : IRequest<PatientModel>
    {
        public long Id { get; set; }
    }
    
    public class GetPatientByIdHandler : IRequestHandler<GetPatientByIdQuery, PatientModel>
    {
        private readonly IPatientRepository _repository;

        public GetPatientByIdHandler(IPatientRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<PatientModel> Handle(GetPatientByIdQuery request, CancellationToken cancellationToken)
        {
            var patient = await _repository.GetPatientByIdAsync(request.Id);
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