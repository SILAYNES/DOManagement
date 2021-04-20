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
    public class PatientRepository : IPatientRepository
    {
        private readonly DatabaseConfig _databaseConfig;

        public PatientRepository(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }

        public async Task<Patient> GetPatientByIdAsync(long patientId)
        {
            using IDbConnection connection = new SqliteConnection(_databaseConfig.Name);
            var sql = @"SELECT p.id, p.names, p.surnames, p.age, p.birthday, p.enabled, p.created, 
                p.created_by, p.last_modified, p.last_modified_by, a.id, a.name, a.enabled, a.created, 
                a.created_by, a.last_modified, a.last_modified_by
                FROM patient p
                JOIN patient_allergies pa ON p.id = pa.patient_id
                JOIN allergy a ON pa.allergy_id = a.id
                WHERE p.id = @Id;";
            var patientDictionary = new Dictionary<long?, Patient>();
            var patients = await connection.QueryAsync<Patient, Allergy, Patient>(sql, 
                (patient, allergy) => 
                {
                    Patient patientEntry;
                    if (!patientDictionary.TryGetValue(patient.Id, out patientEntry))
                    {
                        patientEntry = patient;
                        patientDictionary.Add(patientEntry.Id, patientEntry);
                    }
                    patientEntry.Allergies.Add(allergy);
                    return patientEntry;
                }, 
                new { Id = patientId }, splitOn: "a.id");

            return patients.Distinct().SingleOrDefault();
        }

        public async Task<IEnumerable<Patient>> GetPatientsAsync()
        {
            using IDbConnection connection = new SqliteConnection(_databaseConfig.Name);
            var sql = @"SELECT p.id, p.names, p.surnames, p.age, p.birthday, p.enabled, p.created, 
                p.created_by, p.last_modified, p.last_modified_by, a.id, a.name, a.enabled, a.created, 
                a.created_by, a.last_modified, a.last_modified_by
                FROM patient p
                JOIN patient_allergies pa ON p.id = pa.patient_id
                JOIN allergy a ON pa.allergy_id = a.id;";
            var patientDictionary = new Dictionary<long?, Patient>();
            var patients = await connection.QueryAsync<Patient, Allergy, Patient>(sql, 
                (patient, allergy) => 
                {
                    Patient patientEntry;
                    if(!patientDictionary.TryGetValue(patient.Id, out patientEntry))
                    {
                        patientEntry = patient;
                        patientDictionary.Add(patientEntry.Id, patientEntry);
                    }
                    patientEntry.Allergies.Add(allergy);
                    return patientEntry;
                }, splitOn: "a.id");

            return patients.Distinct();
        }

        public async Task<long> CreatePatientAsync(Patient patient)
        {
            using IDbConnection connection = new SqliteConnection(_databaseConfig.Name);
            var sql = @"INSERT INTO patient (names, surnames, age, birthday, created_by)
                VALUES (@Names, @Surnames, @Age, @Birthday, @CreatedBy);";
            return await connection.QuerySingleAsync<long>(sql, patient);
        }

        public async Task<long> UpdatePatientAsync(Patient patient)
        {
            using IDbConnection connection = new SqliteConnection(_databaseConfig.Name);
            var sql = @"INSERT OR REPLACE INTO patient (id, names, surnames, age, birthday, enabled, created_by, last_modified, last_modified_by)
                VALUES (@Id, @Names, @Surnames, @Age, @Birthday, @Enabled, @CreatedBy, @LastModified, @LastModifiedBy);";
            return await connection.QuerySingleAsync<long>(sql, patient);
        }

        public async Task<long> UpdatePatientAsync(long patientId)
        {
            using IDbConnection connection = new SqliteConnection(_databaseConfig.Name);
            var sql = @"UPDATE patient SET enabled = false WHERE id = @Id;";
            return await connection.QuerySingleAsync<long>(sql, new { Id = patientId });
        }

        public async Task<long> CreatePatientAllergyAsync(long patientId, Allergy allergy)
        {
            using IDbConnection connection = new SqliteConnection(_databaseConfig.Name);
            var sql = @"INSERT OR REPLACE INTO patient_allergies (patient_id, allergy_id, created_by)
            VALUES (@PatientId, @AllergyId, @CreatedBy);";
            return await connection.QuerySingleAsync<long>(sql, new
            {
                PatientId = patientId,
                AllergyId = allergy.Id,
                CreatedBy = "RELATIONSHIP"
            });
        }

        public async Task<long> DeletePatientAllergyAsync(long patientId, Allergy allergy)
        {
            using IDbConnection connection = new SqliteConnection(_databaseConfig.Name);
            var sql = @"DELETE patient_allergies WHERE patient_id = @PatientId and allergy_id = @AllergyId;";
            return await connection.QuerySingleAsync<long>(sql, new { PatientId = patientId, AllergyId = allergy.Id });
        }
    }
}

