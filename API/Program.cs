using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbcontext>(opt =>
{
   opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddCors();
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod()
.WithOrigins("http://localhost:3000" , "https://localhost:3000"));

app.MapControllers();

using var scop = app.Services.CreateScope();
var services = scop.ServiceProvider;

try
{
   var context = services.GetRequiredService<AppDbcontext>();
   await context.Database.MigrateAsync();
   await DbInitializer.SeedData(context);
}
catch (Exception ex)
{
   var logger =services.GetRequiredService<ILogger<Program>>();
   logger.LogError(ex,"An error occured during migration.");
}
app.Run();
