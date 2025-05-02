using CaptoneProject.Services.AuthAPI.Data;
using CaptoneProject.Services.AuthAPI.JwtTokenGenerationService;
using CaptoneProject.Services.AuthAPI.SeedRole;
using CaptoneProject.Services.AuthAPI.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CaptoneProject.Services.AuthAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configure the database context and Identity.
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("AuthDB"));
            });

            builder.Services.AddScoped<JwtService>();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

            // Configure JWT authentication.
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    };
                });

            // Ensure the database is created (use migrations for production).
            var serviceProvider = builder.Services.BuildServiceProvider();
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                context.Database.EnsureCreated(); // Good for development; consider migrations in production.
            }

            // Configure CORS to allow requests from the Angular frontend.
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularDev", policy =>
                {
                    // Allow both HTTP and HTTPS origins for local dev environments.
                    policy.WithOrigins("http://localhost:4200", "https://localhost:4200")
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials();
                });
            });

            var app = builder.Build();

            // Apply CORS policy before routing
            app.UseCors("AllowAngularDev");

            // Serve static files if necessary
            app.UseStaticFiles();

            // Configure file uploads with MIME types
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Uploads")),
                RequestPath = "/Files",
                OnPrepareResponse = ctx =>
                {
                    var fileExtension = Path.GetExtension(ctx.File.PhysicalPath).ToLower();
                    if (fileExtension == ".pdf")
                    {
                        ctx.Context.Response.ContentType = "application/pdf";
                    }
                    else if (fileExtension == ".jpg" || fileExtension == ".jpeg")
                    {
                        ctx.Context.Response.ContentType = "image/jpeg";
                    }
                    else if (fileExtension == ".png")
                    {
                        ctx.Context.Response.ContentType = "image/png";
                    }
                    else if (fileExtension == ".docx" || fileExtension == ".doc")
                    {
                        ctx.Context.Response.ContentType = "application/msword";
                    }
                    else if (fileExtension == ".xlsx")
                    {
                        ctx.Context.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    }
                    // Add more conditions for other file types if needed.
                }
            });

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Ensure HTTPS redirection.
            app.UseHttpsRedirection();

            // Authentication should be before routing.
            app.UseAuthentication();

            // Routing should come before Authorization
            app.UseRouting();

            // Authorization middleware
            app.UseAuthorization();

            // Map controllers to endpoints
            app.MapControllers();

            // Run the app
            app.Run();
        }
    }
}
