using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using InnoGotchi.Application.Interfaces;

namespace InnoGotchi.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services,IConfiguration configuration)
        {
            var connectionString = configuration["DbConnection"];

            services.AddDbContext<InnoGotchiDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddTransient<IUsersDbContext>(provider =>
                provider.GetService<InnoGotchiDbContext>());
            services.AddTransient<IFarmsDbContext>(provider =>
                provider.GetService<InnoGotchiDbContext>());
            services.AddTransient<ICollaborationDbContext>(provider =>
                provider.GetService<InnoGotchiDbContext>());
            services.AddTransient<IPetsDbContext>(provider =>
                provider.GetService<InnoGotchiDbContext>());
            services.AddTransient<IPetsStatusesDbContext>(provider =>
                provider.GetService<InnoGotchiDbContext>());
            services.AddTransient<IPetAppearanceDbContext>(provider =>
                provider.GetService<InnoGotchiDbContext>());

            return services;
        }
    }
}
