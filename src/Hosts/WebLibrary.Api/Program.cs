using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebLibrary.Api.Controllers;
using WebLibrary.AppServices.Users.Repositories;
using WebLibrary.AppServices.Users.Services;
using WebLibrary.Contracts.Users;
using WebLibrary.DataAccess;
using WebLibrary.DataAccess.User.Repository;
using WebLibrary.Infrastructure.Repository;
using WebLibrary.ComponentRegistrar;
using WebLibrary.AppServices.Files.Services;
using WebLibrary.AppServices.Files.Repositories;
using WebLibrary.DataAccess.Files.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Serilog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using WebLibrary.AppServices.Jwt;
using WebLibrary.AppServices.Libraries.Services;
using WebLibrary.DataAccess.Library.Repository;
using WebLibrary.AppServices.Libraries.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "WebLibrary API", Version = "V1" });
    options.IncludeXmlComments(Path.Combine(Path.Combine(AppContext.BaseDirectory, $"{typeof(UserController).Assembly.GetName().Name}.xml")));
    options.IncludeXmlComments(Path.Combine(Path.Combine(AppContext.BaseDirectory, $"{typeof(UserDto).Assembly.GetName().Name}.xml")));
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

builder.Services.AddServices();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IFileService, FileService>();
builder.Services.AddTransient<IJwtService, JwtService>();
builder.Services.AddTransient<ILibraryService, LibraryService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IFileRepository, FileRepository>();
builder.Services.AddScoped<ILibraryRepository, LibraryRepository>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection")));

builder.Services.AddScoped<DbContext>(s => s.GetRequiredService<ApplicationDbContext>());

builder.Services.AddAuthentication(x => 
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorization(options =>
{
    /*options.DefaultPolicy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();

    options.AddPolicy("MyPolicy", policy => policy.RequireClaim("MyClaim"));*/
});

builder.Services.AddSerilog((services, config) =>
{
    config.ReadFrom.Configuration(builder.Configuration)
    .WriteTo.Console()
    .WriteTo.Debug();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
