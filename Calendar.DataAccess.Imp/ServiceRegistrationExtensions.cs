using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Calendar.DataAccess.Imp
{
    public static class ServiceRegistrationExtensions
    {
        public static void RegisterGoogleToken(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<GoogleTokenContext>(options =>
                options.UseSqlServer(connectionString));
        }
    }
}
