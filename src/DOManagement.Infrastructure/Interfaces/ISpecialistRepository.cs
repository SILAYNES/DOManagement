using System.Collections.Generic;
using System.Threading.Tasks;
using DOManagement.Domain.Entities;

namespace DOManagement.Infrastructure.Interfaces
{
    public interface ISpecialistRepository
    {
        Task<Specialist> GetSpecialistByIdAsync(long specialistId);
        Task<IEnumerable<Specialist>> GetSpecialistsAsync();
        Task<long> CreateSpecialistAsync(Specialist specialist);
        Task<long> UpdateSpecialistAsync(Specialist specialist);
        Task<long> DeleteSpecialistAsync(long specialistId);
    }
}
