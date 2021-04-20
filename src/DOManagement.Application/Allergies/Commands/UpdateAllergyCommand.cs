using System;
using System.Threading;
using System.Threading.Tasks;
using DOManagement.Application.Common.Models;
using DOManagement.Domain.Entities;
using DOManagement.Infrastructure.Interfaces;
using MediatR;

namespace DOManagement.Application.Allergies.Commands
{
    public class UpdateAllergyCommand : IRequest<AllergyModel>
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
    
    public class UpdateAllergyHandler : IRequestHandler<UpdateAllergyCommand, AllergyModel>
    {
        private readonly IAllergyRepository _repository;

        public UpdateAllergyHandler(IAllergyRepository repository)
        {
            _repository = repository;
        }

        public async Task<AllergyModel> Handle(UpdateAllergyCommand request, CancellationToken cancellationToken)
        {
            var allergy = await _repository.GetAllergyByIdAsync(request.Id);
            allergy.Name = request.Name;
            allergy.LastModified = DateTime.UtcNow;
            allergy.LastModifiedBy = "APPLICATION";
            
            var insertedId = await _repository.UpdateAllergyAsync(allergy);
            return new AllergyModel()
            {
                Id = insertedId,
                Name = allergy.Name
            };
        }
    }
}