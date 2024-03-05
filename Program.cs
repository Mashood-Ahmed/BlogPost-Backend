using Microsoft.EntityFrameworkCore;
using Portfolio.API.Authorization;
using Portfolio.API.Data;
using Portfolio.API.Helpers;
using Portfolio.API.Repository.Implementation;
using Portfolio.API.Repository.Interface;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;
var env = builder.Environment;

services.AddControllers();
//services.AddControllers().AddJsonOptions(options =>
//{
//    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
//});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("PortfolioConnectionString"));
});

// configure DI for application services
services.AddScoped<IJwtUtils, JwtUtils>();

//add services
services.AddScoped<ICateogryRepository, CategoryRepository>();
services.AddScoped<IUserRepository, UserRepository>();
services.AddScoped<IUserProfileRepository, UserProfileRepository>();
services.AddScoped<IFollowerRepository, FollowerRepository>();
services.AddScoped<IPostRepository, PostRepository>();

// configure automapper with all automapper profiles from this assembly
services.AddAutoMapper(typeof(Program));

// configure strongly typed settings object
services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(options =>
{
    options.AllowAnyHeader();
    options.AllowAnyOrigin();
    options.AllowAnyMethod();
});

app.UseMiddleware<ErrorHandlerMiddleware>();

// custom jwt auth middleware
app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.Run();
