using DOManagement.Infrastructure.Configurations;
using DOManagement.Infrastructure.Interfaces;
using DOManagement.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DOManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = false;
            
            services.AddSingleton(new DatabaseConfig() { Name = configuration["DatabaseName"]});
            services.AddSingleton<IPatientRepository, PatientRepository>();
            services.AddSingleton<ISpecialistRepository, SpecialistRepository>();
            services.AddSingleton<IAppointmentRepository, AppointmentRepository>();
            services.AddSingleton<IAllergyRepository, AllergyRepository>();
            
            return services;
        }
    }
}