
using Azure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using sim_backend_api.Data;
using sim_backend_api.Models;
using sim_backend_api.Services.Course;
using sim_backend_api.Services.Student;
using sim_backend_api.Services.Token;
using System.Text;

namespace sim_backend_api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration.AddAzureKeyVault(
                new Uri("https://studentmanagement-tcs-kv.vault.azure.net/"), new DefaultAzureCredential());

            var jwt = builder.Configuration.GetSection("Jwt");
            // Add services to the container.

            builder.Services.AddControllers();
            var constr = builder.Configuration.GetConnectionString("ConStr");

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(constr);
            });

            builder.Services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwt["Issuer"],
                        ValidAudience = jwt["Audience"],
                        IssuerSigningKey =
                        new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwt["Key"]!))


                    };
                });




            builder.Services.AddScoped<IStudnetService, StudnetService>();
            builder.Services.AddScoped<ICourseService, CourseService>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference(options =>
                {
                    options.Title = "SIM API";
                    options.Theme = ScalarTheme.BluePlanet;
                });
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            //app.MapGet("/jwt-test", (IConfiguration config) =>
            //{
            //    return Results.Ok(new
            //    {
            //        Issuer = config["Jwt:Issuer"],
            //        Audience = config["Jwt:Audience"],
            //        Expiry = config["Jwt:ExpiryMinutes"],
            //        ConStr = config["ConnectionStrings:ConStr"]
            //    });
            //});

            app.MapControllers();

            app.Run();
        }
    }
}
