using ContentGenerator.Api.Core.Entities;
using ContentGenerator.Api.Core.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace ContentGenerator.Api.Core.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;

        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;
        }

        public async Task SendEmail(Assunto assunto)
        {
            // Lógica para enviar o email
            _logger.LogInformation("Sending email for AssuntoId: {AssuntoId}", assunto.AssuntoId);
            await Task.CompletedTask;
        }
    }
}
