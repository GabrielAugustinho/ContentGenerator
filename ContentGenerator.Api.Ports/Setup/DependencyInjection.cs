using ContentGenerator.Api.Adapters.Repository;
using ContentGenerator.Api.Adapters.Repository.WhatsAppRepo;
using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.OutputPort.DestinyPort;
using ContentGenerator.Api.Core.UseCases.DestinyCase;
using ContentGenerator.Api.Core.UseCases.WhatsAppCase;
using ContentGenerator.Api.Core.UseCases.WhatsAppCase.Interfaces;

namespace ContentGenerator.Api.Ports.Setup
{
    public static class DependencyInjection
    {
        public static IServiceProvider? ServiceProvider { get; private set; }

        public static void RegisterService(this IServiceCollection services)
        {
            // UseCases
            services.AddScoped<ISearchWhatsAppPaged, SearchWhatsAppPaged>();
            services.AddScoped<IAddWhatsAppNumber, AddWhatsAppNumber>();
            services.AddScoped<IUpdateWhatsAppNumber, UpdateWhatsAppNumber>();
            services.AddScoped<IDeleteWhatsApp, DeleteWhatsApp>();

            services.AddScoped<ISearchDestiny, SearchDestiny>();

            // Abstractions
            services.AddScoped<IWhatsAppRepository, WhatsAppRepository>();
            services.AddScoped<IDestinyRepository, DestinyRepository>();

            // Others


            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
