using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using DOManagement.Domain.Entities;
using DOManagement.Infrastructure.Configurations;
using DOManagement.Infrastructure.Interfaces;
using Microsoft.Data.Sqlite;

namespace DOManagement.Infrastructure.Repositories
{
    public class AllergyRepository : IAllergyRepository
    {
        private readonly DatabaseConfig _databaseConfig;

        public AllergyRepository(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }

        public async Task<Allergy> GetAllergyByIdAsync(long allergyId)
        {
            using IDbConnection connection = new SqliteConnection(_databaseConfig.Name);
            var sql = @"SELECT id, name, enabled, created, created_by, last_modified, last_modified_by FROM allergy 
            WHERE id = @Id;";
            return await connection.QuerySingleAsync<Allergy>(sql, new { Id = allergyId });
        }

        public async Task<IEnumerable<Allergy>> GetAllergiesAsync()
        {
            using IDbConnection connection = new SqliteConnection(_databaseConfig.Name);
            var sql = @"SELECT id, name, enabled, created, created_by, last_modified, last_modified_by FROM allergy;";
            return await connection.QueryAsync<Allergy>(sql);
        }

        public async Task<long> CreateAllergyAsync(Allergy allergy)
        {
            using IDbConnection connection = new SqliteConnection(_databaseConfig.Name);
            var sql = @"INSERT INTO allergy (name, created_by) VALUES (@Name, @CreatedBy);";
            return await connection.QuerySingleAsync<long>(sql, allergy);
        }

        public async Task<long> UpdateAllergyAsync(Allergy allergy)
        {
            using IDbConnection connection = new SqliteConnection(_databaseConfig.Name);
            var sql = @"INSERT OR REPLACE INTO allergy (id, name, enabled, created_by, last_modified, last_modified_by) 
            VALUES (@Id, @Name, @Enabled, @CreatedBy, @LastModified, @LastModifiedBy);";
            return await connection.QuerySingleAsync<long>(sql, allergy);
        }

        public async Task<long> DeleteAllergyAsync(long allergyId)
        {
            using IDbConnection connection = new SqliteConnection(_databaseConfig.Name);
            var sql = @"DELETE FROM allergy WHERE id = @Id";
            return await connection.QuerySingleAsync<long>(sql, new { Id = allergyId});
        }
    }
}
