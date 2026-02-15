using Microsoft.EntityFrameworkCore;
using TaskHub.Infrastructure.Persistence;

using TaskHub.Application.Commands.CreateTask;
using TaskHub.Application.Commands.CompleteTask;
using TaskHub.Application.Commands.RenameTask;
using TaskHub.Application.Commands.DeleteTask;

using TaskHub.Application.Queries.GetTaskList;

namespace TaskHub.Api.Setup;

public static class ServiceRegistration
{
    public static IServiceCollection AddTaskHubServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddOpenApi();

        // DbContext
        services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseSqlite(config.GetConnectionString("Db"));
        });

        // Command handlers
        services.AddScoped<CreateTaskHandler>();
        services.AddScoped<CompleteTaskHandler>();
        services.AddScoped<RenameTaskHandler>();
        services.AddScoped<DeleteTaskHandler>();

        // Query handlers
        services.AddScoped<GetTaskListHandler>();

        return services;
    }
}
