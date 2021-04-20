using System.Collections.Generic;
using System.Threading.Tasks;
using DOManagement.Domain.Entities;

namespace DOManagement.Infrastructure.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<Appointment> GetAppointmentByIdAsync(long appointmentId);
        Task<IEnumerable<Appointment>> GetAppointmentsAsync();
        Task<long> CreateAppointmentAsync(Appointment appointment);
        Task<long> UpdateAppointmentAsync(Appointment appointment);
        Task<long> DeleteAppointmentAsync(long appointmentId);
    }

}
