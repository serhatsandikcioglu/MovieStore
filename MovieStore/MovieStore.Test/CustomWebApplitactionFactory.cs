using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MovieStore.Data.DataBase;
using MovieStore.Data.Entities;

namespace MovieStore.Test
{
    public class CustomWebApplitactionFactory<Tprogram> : WebApplicationFactory<Tprogram> where Tprogram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var serviceDescriptor = services.Single(x => x.ServiceType == typeof(DbContextOptions<AppDbContext>));
                services.Remove(serviceDescriptor);
                services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("movie"));
                var serviceProvider = services.BuildServiceProvider();
                using var scope = serviceProvider.CreateScope();
                var db = serviceProvider.GetRequiredService<AppDbContext>();
                db.Database.EnsureCreated();

                for (int i = 1; i < 6; i++)
                {
                    db.Actors.Add(new Actor { Name = $"Actor_{i}", Surname = $"Surname_{i}" });
                }

                db.SaveChanges();
                builder.UseEnvironment("Development");
            });

        }
    }
}
