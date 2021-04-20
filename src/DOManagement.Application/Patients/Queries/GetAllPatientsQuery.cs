using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DOManagement.Application.Common.Models;
using DOManagement.Infrastructure.Interfaces;
using MediatR;

namespace DOManagement.Application.Patients.Queries
{
    public class GetAllPatientsQuery : IRequest<IEnumerable<PatientModel>>
    {
    }
    
    public class GetAllPatientsHandler : IRequestHandler<GetAllPatientsQuery, IEnumerable<PatientModel>>
    {
        private readonly IPatientRepository _repository;

        public GetAllPatientsHandler(IPatientRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PatientModel>> Handle(GetAllPatientsQuery request, CancellationToken cancellationToken)
        {
            var patients = await _repository.GetPatientsAsync();
            return patients.Select(patient => new PatientModel()
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
            });
        }
    }


}