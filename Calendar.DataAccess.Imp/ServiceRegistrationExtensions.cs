using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Calendar.DataAccess.Facade.provider;

namespace Calendar.DataAccess.Imp
{
    public static class ServiceRegistrationExtensions
    {
        public static void RegisterGoogleToken(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<GoogleTokenContext>(options =>
                options.UseSqlServer(connectionString));
        }
        public static void RegisterDal(this IServiceCollection services)
        {
            services.AddScoped<IGoogleTokenDal, GoogleTokenDal>();
        }

    }
}
