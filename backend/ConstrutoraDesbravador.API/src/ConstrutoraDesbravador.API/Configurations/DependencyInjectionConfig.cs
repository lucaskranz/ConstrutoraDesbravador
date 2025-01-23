using ConstrutoraDesbravador.Business.Interfaces;
using ConstrutoraDesbravador.Business.Notificacoes;
using ConstrutoraDesbravador.Business.Services;
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

            services.AddHttpClient<RandomUserService>(client =>
            {
                client.BaseAddress = new Uri("https://randomuser.me/api/");
            });

            // Business
            services.AddScoped<IFuncionarioService, FuncionarioService>();
            services.AddScoped<IProjetoService, ProjetoService>();
            services.AddScoped<INotificador, Notificador>();

            return services;
        }
    }
}
