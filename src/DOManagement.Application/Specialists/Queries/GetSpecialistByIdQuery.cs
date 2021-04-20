using System.Threading;
using System.Threading.Tasks;
using DOManagement.Application.Common.Models;
using DOManagement.Infrastructure.Interfaces;
using MediatR;

namespace DOManagement.Application.Specialists.Queries
{
    public class GetSpecialistByIdQuery : IRequest<SpecialistModel>
    {
        public long Id { get; set; }
    }
    
    public class GetSpecialistByIdHandler : IRequestHandler<GetSpecialistByIdQuery, SpecialistModel>
    {
        private readonly ISpecialistRepository _repository;

        public GetSpecialistByIdHandler(ISpecialistRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<SpecialistModel> Handle(GetSpecialistByIdQuery request, CancellationToken cancellationToken)
        {
            var specialist = await _repository.GetSpecialistByIdAsync(request.Id);
            return new SpecialistModel()
            {
                Id = specialist.Id,
                Names = specialist.Names,
                Surnames = specialist.Surnames
            };
        }
    }


}