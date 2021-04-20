using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DOManagement.Application.Common.Models;
using DOManagement.Domain.Entities;
using DOManagement.Infrastructure.Interfaces;
using MediatR;

namespace DOManagement.Application.Specialists.Queries
{
    public class GetAllSpecialistQuery : IRequest<IEnumerable<SpecialistModel>>
    {
    }
    
    public class GetAllSpecialistsHandler : IRequestHandler<GetAllSpecialistQuery, IEnumerable<SpecialistModel>>
    {
        private readonly ISpecialistRepository _repository;

        public GetAllSpecialistsHandler(ISpecialistRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SpecialistModel>> Handle(GetAllSpecialistQuery request, CancellationToken cancellationToken)
        {
            var specialists = await _repository.GetSpecialistsAsync();
            return specialists.Select(specialist => new SpecialistModel()
            {
                Id = specialist.Id,
                Names = specialist.Names,
                Surnames = specialist.Surnames
            });
        }
    }


}