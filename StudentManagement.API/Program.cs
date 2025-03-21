
using StudentManagement.API.Configuration;

namespace StudentManagement.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.ConfigInfrastructure(builder.Configuration);
            builder.Services.ConfigureServices(builder.Configuration);
            builder.Services.ConfigApplication(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.ConfigureMiddleware();
            app.MapControllers();

            app.Run();
        }
    }
}
