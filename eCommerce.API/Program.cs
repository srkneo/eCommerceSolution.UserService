using eCommerce.Core;
using eCommerce.Infrastructure;


var builder = WebApplication.CreateBuilder(args);

//Add Infrastructure Services
builder.Services.AddInfrastructure();
builder.Services.AddCore();

// Add Controllers to the service collections
builder.Services.AddControllers();

// Build the web application
var app = builder.Build();

//Routing
app.UseRouting();

//Auth
app.UseAuthentication();
app.UseAuthorization();

//Controller Routes
app.MapControllers();
app.Run();
