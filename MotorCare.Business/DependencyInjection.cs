using Microsoft.Extensions.DependencyInjection;

namespace MotorCare.Business;

public static class DependencyInjection
{
    public static IServiceCollection AddBusiness(this IServiceCollection services)
    {
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IAdminUserService, AdminUserService>();
        services.AddScoped<IProfileService, ProfileService>();
        return services;
    }
}
