using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DOManagement.Application.Common.Models;
using DOManagement.Infrastructure.Interfaces;
using MediatR;

namespace DOManagement.Application.Allergies.Queries
{
    public class GetAllAllergiesQuery : IRequest<IEnumerable<AllergyModel>>
    {
    }
    
    public class GetAllAllergiesHandler : IRequestHandler<GetAllAllergiesQuery, IEnumerable<AllergyModel>>
    {
        private readonly IAllergyRepository _repository;

        public GetAllAllergiesHandler(IAllergyRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<IEnumerable<AllergyModel>> Handle(GetAllAllergiesQuery request, CancellationToken cancellationToken)
        {
            var allergies = await _repository.GetAllergiesAsync();
            return allergies.Select(a => new AllergyModel() {Id = a.Id, Name = a.Name});
        }
    }
}