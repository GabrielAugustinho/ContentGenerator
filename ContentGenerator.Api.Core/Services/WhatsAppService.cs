using ContentGenerator.Api.Core.Entities;
using ContentGenerator.Api.Core.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace ContentGenerator.Api.Core.Services
{
    public class WhatsAppService : IWhatsAppService
    {
        private readonly ILogger<WhatsAppService> _logger;

        public WhatsAppService(ILogger<WhatsAppService> logger)
        {
            _logger = logger;
        }

        public async Task SendWhatsAppMessage(Assunto assunto)
        {
            // Lógica para enviar a mensagem via WhatsApp
            _logger.LogInformation("Sending WhatsApp message for AssuntoId: {AssuntoId}", assunto.AssuntoId);
            await Task.CompletedTask;
        }
    }
}
