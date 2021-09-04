using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using ShopBridges.Business;

namespace ShopBridges
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
            services.AddDbContext<ShopBridgesContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionString")));

            //Dependency injection
            services.AddTransient<ProductBL, ProductBL>();

            //Swagger code
            services.AddSwaggerGen();


            // services.AddControllers();
            services.AddControllers().SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                           .AddNewtonsoftJson(options =>
                           {
                               options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                               options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                           });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Logging file path setup
            loggerFactory.AddFile(Configuration.GetSection("LogFilePath").Value);

            //Swagger configure
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute("Default", "{controller}/{action}");
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("DefaultApi", "{controller=Home}/{action=index}");
            });

        }
    }
}
