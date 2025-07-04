using eCommerce.API.Middlewares;
using eCommerce.Core;
using eCommerce.Core.Mappers;
using eCommerce.Infrastructure;
using FluentValidation.AspNetCore;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

//Add Infrastructure Services
builder.Services.AddInfrastructure();
builder.Services.AddCore();

// Add Controllers to the service collections
builder.Services.AddControllers().AddJsonOptions(
    options => {
        options.JsonSerializerOptions.Converters
        .Add(new JsonStringEnumConverter());
    });

builder.Services.AddAutoMapper(
    typeof(ApplicationUserMappingProfile).Assembly);

//Fluent Validations
builder.Services.AddFluentValidationAutoValidation();

//Add API explorer for Swagger
builder.Services.AddEndpointsApiExplorer();

//Add Swagger
builder.Services.AddSwaggerGen();

//add cors services
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder => builder.WithOrigins("https://localhost:4200")
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});

// Build the web application
var app = builder.Build();

app.UseExceptionHandlingMiddleware();

//Routing
app.UseRouting();
app.UseSwagger(); // adds endpoint that can serve the Swagger UI
app.UseSwaggerUI(); // adds the Swagger UI middleware

//CORS
app.UseCors();

//Auth
app.UseAuthentication();
app.UseAuthorization();

//Controller Routes
app.MapControllers();
app.Run();
