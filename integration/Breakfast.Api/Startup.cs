using AutoMapper;
using Breakfast.Api.Configuration;
using Breakfast.Api.Infrastructure;
using Breakfast.Api.Interfaces;
using Breakfast.Api.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Extensions.Repository.Extensions;

namespace Breakfast.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var assembly = typeof(Startup).Assembly;

            services.Configure<BreakfastApiOptions>(_configuration.GetSection("breakfast"));

            services.AddAutoMapper(c => c.AddProfiles(assembly));

            services.AddMvc(o => { o.Filters.Add<ApiExceptionFilter>(); })
                    .AddFluentValidation(c =>
                                         {
                                             c.RegisterValidatorsFromAssembly(assembly);
                                             c.ImplicitlyValidateChildProperties = true;
                                         });

            services.AddMongoRepositories(_configuration.GetConnectionString("mongo"))
                    .FromAssembly(assembly)
                    .WithIndexesFromAssembly(assembly);

            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IBreakfastItemService, BreakfastItemService>();
            services.AddTransient<IBreakfastOrderService, BreakfastOrderService>();
            services.AddTransient<IBreakfastReviewService, BreakfastReviewService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            app.UseMvc();
        }
    }
}
