using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.InputPort.ContentPort;
using ContentGenerator.Api.Core.InputPort.PublicationPort;
using ContentGenerator.Api.Core.Models;
using ContentGenerator.Api.Core.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace ContentGenerator.Api.Core.UseCases.PublicationCase
{
    public class AddPublication : IAddPublication
    {
        private readonly IContentRepository _contentRepository;
        private readonly IWhatsAppService _whatsAppService;
        private readonly IEmailService _emailService;
        private readonly ILogger<AddPublication> _logger;

        public AddPublication(
            IContentRepository contentRepository,
            IWhatsAppService whatsAppService,
            IEmailService emailService,
            ILogger<AddPublication> logger)
        {
            _contentRepository = contentRepository;
            _whatsAppService = whatsAppService;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<bool> Execute(PublicationInput input, TwillioKeysModel keys)
        {
            try
            {
                var assunto = await _contentRepository.GetContentById(input.AssuntoId);
                if (assunto == null)
                {
                    _logger.LogWarning("Assunto not found. AssuntoId: {AssuntoId}", input.AssuntoId);
                    return false;
                }

                if (string.IsNullOrEmpty(assunto.DataValida.ToString()))
                {
                    _logger.LogWarning("Assunto not validated. AssuntoId: {AssuntoId}", input.AssuntoId);
                    return false;
                }

                switch (assunto.Destinos.Descricao)
                {
                    case "WhatsApp":
                        await _whatsAppService.SendWhatsAppMessage(input, assunto, keys);
                        break;
                    case "Email":
                        await _emailService.SendEmail(assunto);
                        break;
                    default:
                        _logger.LogWarning("Unsupported destination type: {Descricao}", assunto.Destinos.Descricao);
                        return false;
                }

                assunto.DataPublicacao = DateTime.UtcNow;
                var updateResult = await _contentRepository.PublishContent(assunto);

                if (!updateResult)
                {
                    _logger.LogError("Failed to update the publication date for AssuntoId: {AssuntoId}", input.AssuntoId);
                    return false;
                }

                _logger.LogInformation("Content published successfully. AssuntoId: {AssuntoId}", input.AssuntoId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to add publication for AssuntoId: {AssuntoId}", input.AssuntoId);
                return false;
            }
        }
    }
}
