using AutoMapper;
using Deloitte.TaskBoard.Api.MapperProfiles;
using Deloitte.TaskBoard.Api.Models.Dtos;
using Deloitte.TaskBoard.Api.Validators;
using Deloitte.TaskBoard.Domain.Models;
using Deloitte.TaskBoard.Infrastructure;
using Deloitte.TaskBoard.Infrastructure.Exceptions;
using Deloitte.TaskBoard.Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using GlobalExceptionHandler.WebApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Net;

namespace Deloitte.TaskBoard.Api
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
            services.AddDbContext<TaskBoardContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("TaskBoardDatabase")));
            
            services.AddApplicationInsightsTelemetry();
            services.AddMvc(option => option.EnableEndpointRouting = false)
                .AddFluentValidation();
            services.AddAutoMapper(typeof(AutoMapperProfile));

            ConfigureValidators(services);
            ConfigureRepositories(services);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "Deloitte Task Board API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            ConfigureExceptionHandlers(app);

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Deloitte Task Board API V1");
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private void ConfigureValidators(IServiceCollection services)
        {
            services.AddTransient<IValidator<CreateAssignmentDto>, CreateAssignmentDtoValidator>();
            services.AddTransient<IValidator<UpdateAssignmentDto>, UpdateAssignmentDtoValidator>();
        }

        private void ConfigureRepositories(IServiceCollection services)
        {
            services.AddTransient<IRepository<Assignment, Guid>, Repository<TaskBoardContext, Assignment, Guid>>();
            services.AddTransient<IAssignmentRepository, AssignmentRepository>();
        }

        private void ConfigureExceptionHandlers(IApplicationBuilder app)
        {
            app.UseGlobalExceptionHandler(options =>
            {
                options.ContentType = "application/json";
                options.ResponseBody(s => JsonConvert.SerializeObject(new
                {
                    Message = s.Message
                }));

                options.Map<EntityAlreadyExistsException>().ToStatusCode(HttpStatusCode.BadRequest);
                options.Map<Exception>().ToStatusCode(HttpStatusCode.InternalServerError);
            });
        }
    }
}
