using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Catmash.EntityModel;
using Catmash.DataRepository;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Catmash.Algorithms;

namespace Catmash.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddControllers();

            string databaseName = "CatmashDatabase.db";
            string databasePath = Path.Combine(Environment.CurrentDirectory, databaseName);
            services.AddDbContextPool<CatmashEntities>(options => options.UseSqlite($"Data Source={databasePath}"));

            services.AddSingleton<PairNumberTracker>();
            services.AddScoped<ICatmashRepository, CatmashRepository>();
            services.AddScoped<IPairGeneratorStrategy, PatternedPairGenerator>();
            services.AddScoped<EloRatingCalculator>();
            services.AddSingleton<Constants>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Vote}/{id?}");
                endpoints.MapControllers();
            });
        }
    }
}
