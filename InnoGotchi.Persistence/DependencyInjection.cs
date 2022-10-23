﻿using Microsoft.Extensions.DependencyInjection;
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

            services.AddScoped<IUsersDbContext>(provider =>
                provider.GetService<InnoGotchiDbContext>());

            return services;
        }
    }
}
