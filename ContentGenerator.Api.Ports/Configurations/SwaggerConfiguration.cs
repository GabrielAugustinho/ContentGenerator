using System.Diagnostics.CodeAnalysis;

namespace ContentGenerator.Api.Ports.Configurations
{
    [ExcludeFromCodeCoverage]
    public static class SwaggerConfiguration
    {
        public static void AddSwaggerConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        }

        public static void UseSwaggerConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
        }
    }
}
