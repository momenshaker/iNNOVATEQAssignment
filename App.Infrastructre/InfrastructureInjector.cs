using App.Core.Domain.Entities;
using App.Core.Domain.Interfaces.ContextInterface;
using App.Infrastructre.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructre
{
    public static class InfrastructureInjector
    {
        /// <summary>
        /// Inject database instance into DI
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextPool<IApplicationDbContext, ApplicationDbContext>
          (options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")), 128);
            return services;
        }
        /// <summary>
        /// Prepare database by adding rows and initiate migration if there is any migration not applied
        /// </summary>
        /// <param name="app"></param>
        /// <param name="jsonData"></param>
        /// <returns></returns>
        public static async Task<IApplicationBuilder> PrepareDatabase(this IApplicationBuilder app, string jsonData)
        {

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                context.Database.Migrate();
                var dummyData = JsonConvert.DeserializeObject<List<Users>>(jsonData);


                if (!context.Users.Any())
                {
                    foreach (var client in dummyData)
                    {
                        context.Users.Add(client);
                    }
                    context.SaveChanges();
                }
           
                return app;
            }
        }
    }
}
