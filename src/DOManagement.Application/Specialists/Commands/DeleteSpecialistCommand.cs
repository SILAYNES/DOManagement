using System.Threading;
using System.Threading.Tasks;
using DOManagement.Application.Common.Models;
using DOManagement.Infrastructure.Interfaces;
using MediatR;

namespace DOManagement.Application.Specialists.Commands
{
    public class DeleteSpecialistCommand : IRequest<SpecialistModel>
    {
        public long Id { get; set; }   
    }
    
    public class DeleteSpecialistHandler : IRequestHandler<DeleteSpecialistCommand, SpecialistModel>
    {
        private readonly ISpecialistRepository _repository;

        public DeleteSpecialistHandler(ISpecialistRepository repository)
        {
            _repository = repository;
        }

        public async Task<SpecialistModel> Handle(DeleteSpecialistCommand request, CancellationToken cancellationToken)
        {
            var resultId = await _repository.DeleteSpecialistAsync(request.Id);
            var specialist = await _repository.GetSpecialistByIdAsync(resultId);
            return new SpecialistModel()
            {
                Id = specialist.Id,
                Names = specialist.Names,
                Surnames = specialist.Surnames
            };
        }
    }
}