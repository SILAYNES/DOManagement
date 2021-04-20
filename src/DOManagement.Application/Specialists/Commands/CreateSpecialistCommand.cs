using System.Threading;
using System.Threading.Tasks;
using DOManagement.Application.Common.Models;
using DOManagement.Domain.Entities;
using DOManagement.Infrastructure.Interfaces;
using MediatR;

namespace DOManagement.Application.Specialists.Commands
{
    public class CreateSpecialistCommand : IRequest<SpecialistModel>
    {
        public long Id { get; set; }   
        public string Names { get; set; }
        public string Surnames { get; set; }
    }
    
    public class CreateSpecialistHandler : IRequestHandler<CreateSpecialistCommand, SpecialistModel>
    {
        private readonly ISpecialistRepository _repository;

        public CreateSpecialistHandler(ISpecialistRepository repository)
        {
            _repository = repository;
        }

        public async Task<SpecialistModel> Handle(CreateSpecialistCommand request, CancellationToken cancellationToken)
        {
            long insertedId = await _repository.CreateSpecialistAsync(new Specialist()
            {
                Id = request.Id,
                Names = request.Names,
                Surnames = request.Surnames,
                CreatedBy = "APPLICATION"
            });
            
            var specialist = await _repository.GetSpecialistByIdAsync(insertedId);
            return new SpecialistModel()
            {
                Id = specialist.Id,
                Names = specialist.Names,
                Surnames = specialist.Surnames
            };
        }
    }
}