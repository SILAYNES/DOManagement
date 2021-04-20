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
    public class SpecialistRepository : ISpecialistRepository
    {
        private readonly DatabaseConfig _databaseConfig;

        public SpecialistRepository(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }
        
        public async Task<Specialist> GetSpecialistByIdAsync(long specialistId)
        {
            using IDbConnection connection = new SqliteConnection(_databaseConfig.Name);
            var sql = @"SELECT id, names, surnames, enabled, created, created_by, last_modified, last_modified_by 
            FROM specialist WHERE id = @Id;";
            return await connection.QuerySingleAsync<Specialist>(sql, new { Id = specialistId });
        }

        public async Task<IEnumerable<Specialist>> GetSpecialistsAsync()
        {
            using IDbConnection connection = new SqliteConnection(_databaseConfig.Name);
            var sql = @"SELECT id, names, surnames, enabled, created, created_by, last_modified, last_modified_by 
            FROM specialist;";
            return await connection.QueryAsync<Specialist>(sql);
        }

        public async Task<long> CreateSpecialistAsync(Specialist specialist)
        {
            using IDbConnection connection = new SqliteConnection(_databaseConfig.Name);
            var sql = @"INSERT INTO specialist (names, surnames, created_by) VALUES (@Names, @Surnames, @CreatedBy);";
            return await connection.QuerySingleAsync<long>(sql);
        }

        public async Task<long> UpdateSpecialistAsync(Specialist specialist)
        {
            using IDbConnection connection = new SqliteConnection(_databaseConfig.Name);
            var sql = @"INSERT OR REPLACE INTO specialist (id, names, surnames, enabled, created, created_by, last_modified, last_modified_by) 
            VALUES (@Id, @Names, @Surnames, @Enabled, @Created, @CreatedBy, @LastModified, @LastModifiedBy);";
            return await connection.QuerySingleAsync<long>(sql, specialist);
        }

        public async Task<long> DeleteSpecialistAsync(long specialistId)
        {
            using IDbConnection connection = new SqliteConnection(_databaseConfig.Name);
            var sql = @"UPDATE specialist SET enabled = false WHERE id = @Id;";
            return await connection.QuerySingleAsync<long>(sql, new { Id = specialistId});
        }
    }
}