using Microsoft.EntityFrameworkCore;
using UserCommentApplication.Models;

var commandLineOptions = CommandLineOptions.Create(args);

Console.WriteLine($"Running application with additional options {commandLineOptions}");

// PSQL doesn't support DateTime in the default format.
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(
  options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddControllers();

builder.Services.AddScoped<ErrorService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}
else
{
  // Disable https redirection because this computer does not have certs configured.
  app.UseHttpsRedirection();
}

// Map all route controllers.
app.MapControllers();

// If the command line has the script argument run the specified script.
if (commandLineOptions.script != null)
{
  Scripts.RunScript(commandLineOptions.script, app);
}
else
{
  // Run the application.
  app.Run();
}
