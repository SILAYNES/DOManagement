using System.Threading;
using System.Threading.Tasks;
using DOManagement.Application.Common.Models;
using DOManagement.Infrastructure.Interfaces;
using MediatR;

namespace DOManagement.Application.Allergies.Queries
{
    public class GetAllergyByIdQuery : IRequest<AllergyModel>
    {
        public long Id { get; set; }
    }
    
    public class GetAllergyByIdHandler : IRequestHandler<GetAllergyByIdQuery, AllergyModel>
    {
        private readonly IAllergyRepository _repository;

        public GetAllergyByIdHandler(IAllergyRepository repository)
        {
            _repository = repository;
        }

        public async Task<AllergyModel> Handle(GetAllergyByIdQuery request, CancellationToken cancellationToken)
        {
            var allergy = await _repository.GetAllergyByIdAsync(request.Id);
            return new AllergyModel()
            {
                Id = allergy.Id,
                Name = allergy.Name
            };
        }
    }
}