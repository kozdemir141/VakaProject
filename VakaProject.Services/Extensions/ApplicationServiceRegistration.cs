using VakaProject.Data.Abstract;
using VakaProject.Data.Concrete;
using Microsoft.Extensions.DependencyInjection;
using VakaProject.Services.Abstract;
using VakaProject.Services.Concrete;

namespace VakaProject.Services.Extensions;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ManagerBase>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IDataProfileService, DataProfileService>();
        services.AddScoped<IIndividualDataService, IndividualDataService>();
        services.AddSingleton<ILevenshteinService, LevenshteinService>();
        services.AddScoped<IJaroWinklerService, JaroWinklerService>();
        services.AddScoped<IProfileMatchingService, ProfileMatchingService>();
        return services;
    }
}