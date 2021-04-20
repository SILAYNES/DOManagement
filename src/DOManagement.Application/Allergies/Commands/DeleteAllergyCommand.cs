using System.Threading;
using System.Threading.Tasks;
using DOManagement.Application.Common.Models;
using DOManagement.Infrastructure.Interfaces;
using MediatR;

namespace DOManagement.Application.Allergies.Commands
{
    public class DeleteAllergyCommand : IRequest<AllergyModel>
    {
        public long Id { get; set; }
    }
    
    public class DeleteAllergyHandler : IRequestHandler<DeleteAllergyCommand, AllergyModel>
    {
        private readonly IAllergyRepository _repository;

        public DeleteAllergyHandler(IAllergyRepository repository)
        {
            _repository = repository;
        }

        public async Task<AllergyModel> Handle(DeleteAllergyCommand request, CancellationToken cancellationToken)
        {
            var insertedId = await _repository.DeleteAllergyAsync(request.Id);
            var allergy = await _repository.GetAllergyByIdAsync(insertedId);
            return new AllergyModel()
            {
                Id = allergy.Id,
                Name = allergy.Name
            };
        }
    }
}