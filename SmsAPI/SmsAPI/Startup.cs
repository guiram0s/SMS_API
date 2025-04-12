using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SmsAPI.BL;
using Microsoft.EntityFrameworkCore;
using SmsAPI.Contexts;
using SmsAPI.Services.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmsAPI.Services.Tracking;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using SmsAPI.Middleware;

namespace SmsAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddSingleton(c => Configuration);

            services.AddScoped<IMessages, Messages>();
            services.AddScoped<ISmsBulk, SmsBulk>();
            services.AddScoped<ISmsApi, SmsApi>();
            services.AddScoped<ISmsTwilio, SmsTwilio>();
            services.AddScoped<IMessageTracking, MessageTracking>();

            services.AddHttpClient();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SmsAPI", Version = "v1" });
            });

            var connectionString = Configuration["connectionStrings:smsInfoDBConnectionString"];//+";"+"MultipleActiveResultSets=True";
            services.AddDbContext<SmsContext>(o =>
            {
                o.UseSqlServer(connectionString);
            });
            services.AddHostedService<TimerUpdateCostServices>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SmsAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
