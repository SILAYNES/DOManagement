using System.Threading;
using System.Threading.Tasks;
using DOManagement.Application.Common.Models;
using DOManagement.Domain.Entities;
using DOManagement.Infrastructure.Interfaces;
using MediatR;

namespace DOManagement.Application.Allergies.Commands
{
    public class CreateAllergyCommand : IRequest<AllergyModel>
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
    
    public class CreateAllergyHandler : IRequestHandler<CreateAllergyCommand, AllergyModel>
    {
        private readonly IAllergyRepository _repository;

        public CreateAllergyHandler(IAllergyRepository repository)
        {
            _repository = repository;
        }

        public async Task<AllergyModel> Handle(CreateAllergyCommand request, CancellationToken cancellationToken)
        {
            long insertedId = await _repository.CreateAllergyAsync(new Allergy()
            {
                Id = request.Id,
                Name = request.Name,
                CreatedBy = "APPLICATION",
            });
            
            var allergy = await _repository.GetAllergyByIdAsync(insertedId);
            return new AllergyModel()
            {
                Id = allergy.Id,
                Name = allergy.Name
            };
        }
    }
}