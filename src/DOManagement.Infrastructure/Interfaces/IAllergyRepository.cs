using System.Collections.Generic;
using System.Threading.Tasks;
using DOManagement.Domain.Entities;

namespace DOManagement.Infrastructure.Interfaces
{
    public interface IAllergyRepository
    {
        Task<Allergy> GetAllergyByIdAsync(long allergyId);
        Task<IEnumerable<Allergy>> GetAllergiesAsync();
        Task<long> CreateAllergyAsync(Allergy allergy);
        Task<long> UpdateAllergyAsync(Allergy allergy);
        Task<long> DeleteAllergyAsync(long allergyId);
    }
}
