using System.Buffers;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DOManagement.Domain.Entities;
using DOManagement.Infrastructure.Configurations;
using DOManagement.Infrastructure.Interfaces;
using Microsoft.Data.Sqlite;

namespace DOManagement.Infrastructure.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly DatabaseConfig _databaseConfig;

        public AppointmentRepository(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }
        
        public async Task<Appointment> GetAppointmentByIdAsync(long appointmentId)
        {
            using IDbConnection connection = new SqliteConnection(_databaseConfig.Name);
            var sql = @"SELECT ap.id, ap.date_at, ap.description, ap.completed, ap.enabled, ap.created, ap.created_by, ap.last_modified, ap.last_modified_by , 
                    sp.id, sp.names, sp.surnames, pa.id, pa.names, pa.surnames
                    FROM appointment ap
                    JOIN specialist sp ON ap.specialist_id = sp.id
                    JOIN patient pa ON ap.patient_id = pa.id
                    WHERE ap.id = @Id;";
            
            var result = await connection.QueryAsync<Appointment, Specialist, Patient, Appointment>(sql, 
                (appointment, specialist, patient) => 
                {
                    specialist.Appointments.Add(appointment);
                    patient.Appointments.Add(appointment);
                    appointment.Specialist = specialist;
                    appointment.Patient = patient;
                    return appointment;
                }, 
                new { Id = appointmentId } , splitOn: "sp.id,pa.id");
            
            return result.SingleOrDefault();
            
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsAsync()
        {
            using IDbConnection connection = new SqliteConnection(_databaseConfig.Name);
            var sql = @"SELECT ap.id, ap.date_at, ap.description, ap.completed, ap.enabled, ap.created, ap.created_by, ap.last_modified, ap.last_modified_by , 
                    sp.id, sp.names, sp.surnames, pa.id, pa.names, pa.surnames
                    FROM appointment ap
                    JOIN specialist sp ON ap.specialist_id = sp.id
                    JOIN patient pa ON ap.patient_id = pa.id;";
            
            var result = await connection.QueryAsync<Appointment, Specialist, Patient, Appointment>(sql, 
                (appointment, specialist, patient) => 
                {
                    specialist.Appointments.Add(appointment);
                    patient.Appointments.Add(appointment);
                    appointment.Specialist = specialist;
                    appointment.Patient = patient;
                    return appointment;
                },
                splitOn: "sp.id,pa.id");
            
            return result;
        }
        
        public async Task<long> CreateAppointmentAsync(Appointment appointment)
        {
            using IDbConnection connection = new SqliteConnection(_databaseConfig.Name);
            var sql = @"INSERT INTO appointment (date_at, description, patient_id, specialist_id, created_by) 
                VALUES (@DateAt, @Description, @PatientId, @SpecialistId, @CreatedBy);";
            
            var result = await connection.QuerySingleAsync<long>(sql, new
            {
                DateAt = appointment.DateAt,
                Description = appointment.Description,
                PatientId = appointment.Patient.Id,
                SpecialistId = appointment.Specialist.Id,
                CreatedBy = appointment.CreatedBy
            });
            
            return result;
        }

        public async Task<long> UpdateAppointmentAsync(Appointment appointment)
        {
            using IDbConnection connection = new SqliteConnection(_databaseConfig.Name);
            var sql = @"INSERT OR REPLACE INTO appointment (date_at, description, patient_id, specialist_id, created_by, last_modified, last_modified_by) 
                VALUES (@DateAt, @Description, @PatientId, @SpecialistId, @CreatedBy, @LastModified, @LastModifiedBy);";
            
            var result = await connection.QuerySingleAsync<long>(sql, new
            {
                DateAt = appointment.DateAt,
                Description = appointment.Description,
                PatientId = appointment.Patient.Id,
                SpecialistId = appointment.Specialist.Id,
                CreatedBy = appointment.CreatedBy,
                LastModified = appointment.LastModified,
                LastModifiedBy = appointment.LastModifiedBy
            });
            
            return result;
        }

        public async Task<long> DeleteAppointmentAsync(long appointmentId)
        {
            using IDbConnection connection = new SqliteConnection(_databaseConfig.Name);
            var sql = @"UPDATE appointment SET enabled = false WHERE id = @Id;";
            return await connection.QuerySingleAsync<long>(sql, new { Id = appointmentId });
        }
    }
}
