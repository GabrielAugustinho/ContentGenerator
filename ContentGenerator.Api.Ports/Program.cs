using ContentGenerator.Api.Database.Context;
using ContentGenerator.Api.Ports.Configurations;
using ContentGenerator.Api.Ports.Setup;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Quando for subir para produção habilitar apenas os sites específicos que deverão acessar.
/*
builder.Services.AddCors(options =>
{
    options.AddPolicy("SpecificOrigin", policy =>
    {
        policy.WithOrigins("https://meu-site.com", "https://outro-dominio.com", ...)
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
*/


builder.Services.AddControllers();

builder.AddSwaggerConfiguration();
builder.Services.RegisterService();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ContentGeneratorConnection"));
});

var app = builder.Build();

// Ativando CORS
app.UseCors("AllowAll");

app.UseSwaggerConfiguration(app.Environment);
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
