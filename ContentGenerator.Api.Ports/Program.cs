using ContentGenerator.Api.Database.Context;
using ContentGenerator.Api.Ports.Configurations;
using ContentGenerator.Api.Ports.Setup;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.AddSwaggerConfiguration();
builder.Services.RegisterService();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ContentGeneratorConnection"));
});

var app = builder.Build();

app.UseSwaggerConfiguration(app.Environment);

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

[ExcludeFromCodeCoverage]
public partial class Program { }