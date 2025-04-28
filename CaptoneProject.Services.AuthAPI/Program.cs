
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
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("AuthDB"));
            });
            builder.Services.AddScoped<JwtService>();
            builder.Services.AddIdentity<ApplicationUser,IdentityRole>(
                options =>
                {
                    options.User.RequireUniqueEmail = true;
                    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(
                options =>
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
            var serviceProvider = builder.Services.BuildServiceProvider();
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                context.Database.EnsureCreated();
            }

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularDev", policy =>
                {
                    policy.WithOrigins("https://localhost:4200")
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials();
                });
            });
           

            var app = builder.Build();
            app.UseStaticFiles();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "Uploads")),
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
                    // You can add more conditions for different file types as needed.
                }
            });


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseCors("AllowAngularDev");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
