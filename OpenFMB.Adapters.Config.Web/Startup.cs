using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using OpenFMB.Adapters.Config.Web.Models;
using OpenFMB.Adapters.Config.Web.Services;

namespace OpenFMB.Adapters.Config.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<HttpClient>();
            services.AddRazorPages();           
            services.AddServerSideBlazor();
            services.AddBlazoredLocalStorage();
            services.AddHttpContextAccessor();
            services.AddSignalR(c =>
            {
                c.EnableDetailedErrors = true;
                c.StreamBufferCapacity = Int32.MaxValue;
                c.MaximumReceiveMessageSize = long.MaxValue;

            });

            // requires using Microsoft.Extensions.Options
            services.Configure<ProjectDatabaseSettings>(
                Configuration.GetSection(nameof(ProjectDatabaseSettings)));

            services.AddSingleton<IProjectDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<ProjectDatabaseSettings>>().Value);

            services.AddSingleton<ProjectService>();
            services.AddControllers();            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseFileServer();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();                
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
