using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PodfilterCore.Data;
using PodfilterCore.Helpers;
using PodfilterCore.Models;
using PodfilterRepository;
using PodfilterWeb.Helpers;

namespace Podfilter
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.AddTransient<IHttpContentProvider<string>, HttpContentProvider<string>>();
            services.AddTransient<IContentDeserializer<string>, StringContentDeserializer>();
            services.AddTransient<BaseModificationMethodTranslator, ModificationMethodTranslator>();
            services.AddTransient<BaseDisplayablePodcastModificationDeserializer, DisplayablePodcastModificationDeserializer>();
            services.AddTransient<BaseCore, Core>();
            services.AddTransient<IDateTimeEpochConverter, DateTimeEpochConverter>();

            // Configure the session management.
            services.AddDistributedMemoryCache();
            services.AddSession(options => 
                {
                    options.IdleTimeout = TimeSpan.FromMinutes(2);
                    options.Cookie.HttpOnly = true;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseSession();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}