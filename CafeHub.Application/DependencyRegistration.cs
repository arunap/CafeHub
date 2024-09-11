using CafeHub.Application.Commands.Cafe;
using CafeHub.Application.Commands.Employee;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CafeHub.Application
{
    public static class DependencyRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // register fluent validations for the models
            // services.AddValidatorsFromAssembly(typeof(CreateCafeCommandValidator).Assembly);
            services.AddValidatorsFromAssemblyContaining<CreateEmployeeCommandValidator>();

            services.AddFluentValidationAutoValidation();

            // register mediator dependency
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateCafeCommand).Assembly));

            return services;
        }
    }
}