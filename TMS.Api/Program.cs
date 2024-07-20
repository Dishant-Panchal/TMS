using TMS.Api.Helpers;
using TMS.Domain.Models;
using TMS.Infrastructure.Repositories;
using TMS.Infrastructure.Services;

namespace TMS.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddDbContext<TMSContext>(ServiceLifetime.Singleton);

            builder.Services.AddTransient<ITaskRepository, TaskRepository>();
            builder.Services.AddTransient<ITaskService, TaskService>();
            builder.Services.AddTransient<ITaskNotesRepository, TaskNotesRepository>();
            builder.Services.AddTransient<ITaskNotesService, TaskNotesService>();
            builder.Services.AddTransient<ITaskAttachmentRepository, TaskAttachmentRepository>();
            builder.Services.AddTransient<ITaskAttachmentService, TaskAttachmentService>();
            builder.Services.AddTransient<IErrorLogRepository, ErrorLogRepository>();
            builder.Services.AddTransient<IErrorLogService, ErrorLogService>();

            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseExceptionHandler();

            app.MapControllers();

            app.Run();
        }
    }
}
