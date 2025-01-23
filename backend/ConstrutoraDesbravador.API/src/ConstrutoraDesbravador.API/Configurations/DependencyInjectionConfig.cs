using ConstrutoraDesbravador.Business.Interfaces;
using ConstrutoraDesbravador.Data.Context;
using ConstrutoraDesbravador.Data.Repository;

namespace ConstrutoraDesbravador.API.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            // Data
            services.AddScoped<ConstrutoraDesbravadorContext>();
            services.AddScoped<IProjetoRepository, ProjetoRepository>();
            services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();

            return services;
        }
    }
}
