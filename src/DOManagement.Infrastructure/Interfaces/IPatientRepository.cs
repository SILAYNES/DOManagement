using System.Collections.Generic;
using System.Threading.Tasks;
using DOManagement.Domain.Entities;

namespace DOManagement.Infrastructure.Interfaces
{
    public interface IPatientRepository
    {
        Task<Patient> GetPatientByIdAsync(long patientId);
        Task<IEnumerable<Patient>> GetPatientsAsync();
        Task<long> CreatePatientAsync(Patient patient);
        Task<long> UpdatePatientAsync(Patient patient);
        Task<long> UpdatePatientAsync(long patientId);
        Task<long> CreatePatientAllergyAsync(long patientId, Allergy allergy);
        Task<long> DeletePatientAllergyAsync(long patientId, Allergy allergy);
    }
}
