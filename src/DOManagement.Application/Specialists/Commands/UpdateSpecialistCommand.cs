using System;
using System.Threading;
using System.Threading.Tasks;
using DOManagement.Application.Common.Models;
using DOManagement.Domain.Entities;
using DOManagement.Infrastructure.Interfaces;
using MediatR;

namespace DOManagement.Application.Specialists.Commands
{
    public class UpdateSpecialistCommand : IRequest<SpecialistModel>
    {
        public long Id { get; set; }   
        public string Names { get; set; }
        public string Surnames { get; set; }
    }
    
    public class UpdateSpecialistHandler : IRequestHandler<UpdateSpecialistCommand, SpecialistModel>
    {
        private readonly ISpecialistRepository _repository;

        public UpdateSpecialistHandler(ISpecialistRepository repository)
        {
            _repository = repository;
        }

        public async Task<SpecialistModel> Handle(UpdateSpecialistCommand request, CancellationToken cancellationToken)
        {
            var specialist = await _repository.GetSpecialistByIdAsync(request.Id);
            specialist.Names = request.Names;
            specialist.Surnames = request.Surnames;
            specialist.LastModified = DateTime.UtcNow;
            specialist.LastModifiedBy = "APPLICATION";

            long insertedId = await _repository.UpdateSpecialistAsync(specialist);
            specialist = await _repository.GetSpecialistByIdAsync(insertedId);
            return new SpecialistModel()
            {
                Id = specialist.Id,
                Names = specialist.Names,
                Surnames = specialist.Surnames
            };
        }
    }
}