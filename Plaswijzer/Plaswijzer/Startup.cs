using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Plaswijzer.MessengerManager;
using Plaswijzer.Data;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Plaswijzer
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

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Register ToiletContext as a service
             services.AddDbContext<ToiletContext>(options =>
                options.UseSqlite("Data Source=toilets.db"));
                

            services.AddMvc();

            // Add custom services
            services.AddScoped<IPayloadHandler, PayloadHandler>();
            services.AddScoped<IMessageHandler, MessageHandler>();
            services.AddTransient<IQueryManager, QueryManager>();
            services.AddTransient<IReplyManager, ReplyManager>();
            services.AddSingleton<IUserTemp, UserTemp>();
            services.AddSingleton<IDataConstants, DataConstants>();
            services.AddSingleton<ITextHandler, RandomTextHandler>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
          //  loggerFactory.AddFile("Logs/plaswijzer-{Date}.txt", LogLevel.Information);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                        template: "{controller=Home}/{action=Index}");
            });
        }
    }
}
