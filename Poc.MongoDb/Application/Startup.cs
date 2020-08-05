using System;
using Application.Interfaces;
using Application.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Application
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSingleton<IMongoClient>(c =>
            {
                var login = "";
                var password = Uri.EscapeDataString("");
                var server = "";

                return new MongoClient($"mongodb+srv://{login}:{password}@{server}/test?retryWrites=true&w=majority");
            });
            
            services.AddScoped(c => 
                c.GetService<IMongoClient>().StartSession());

            services.AddTransient<IRepositoryUser, UserRepository>();
            services.AddTransient<IRepositoryAuthor, AuthorRepository>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "MongoDB POC",
                        Version = "v1"
                    });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
                endpoints.MapControllers()
            );

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MongoDB POC");
                c.DocumentTitle = "MongoDB POC";
                c.DocExpansion(DocExpansion.List);
            });
        }
    }
}
