using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StudentManagement.Application.Interfaces;
using StudentManagement.Application.Services;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Infrastructure.InfrastructureModels;
using StudentManagement.Infrastructure.Persistance;
using StudentManagement.Infrastructure.Persistance.Repositories;
using StudentManagement.Infrastructure.Services;
using System.Text;

namespace StudentManagement.API.Configuration
{
    public static class Configure
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var allowedHost = configuration.GetValue<string>("AllowedHosts");
            services.AddCors(
                options =>
                    options.AddPolicy("AllowAllHostPolicy", policy => policy.WithOrigins(allowedHost)
            ));

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(setup =>
            {
                // Include 'SecurityScheme' to use JWT Authentication
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                                            {
                                                { jwtSecurityScheme, Array.Empty<string>() }
                                            });
            });
        }

        public static void ConfigureMiddleware(this IApplicationBuilder app)
        {
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseCors();
        }

        public static void ConfigApplication(this IServiceCollection services, IConfiguration configuration)
        {
            // JWT configuration
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
            var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IStudentService, StudentService>();
        }

        public static void ConfigInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<StudentManagementDbContext>(
                options =>
                    options.UseSqlite(connectionString)
            );

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();

        }
    }
}
