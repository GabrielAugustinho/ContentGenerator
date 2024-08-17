using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.InputPort.WhatsAppPort;
using ContentGenerator.Api.Core.UseCases.WhatsAppCase.Interfaces;
using Microsoft.Extensions.Logging;

namespace ContentGenerator.Api.Core.UseCases.WhatsAppCase
{
    public class AddWhatsAppNumber : IAddWhatsAppNumber
    {
        private readonly IWhatsAppRepository _whatsAppRepository;
        private readonly ILogger<AddWhatsAppNumber> _logger;

        public AddWhatsAppNumber(IWhatsAppRepository whatsAppRepository, ILogger<AddWhatsAppNumber> logger)
        {
            _whatsAppRepository = whatsAppRepository;
            _logger = logger;
        }

        public async Task<bool> Execute(AddWhatsAppInput input)
        {
            _logger.LogInformation("Iniciando a adição do contato WhatsApp.");

            try
            {
                var result = await _whatsAppRepository.AddWhatsAppNumber(input);

                if (!result)
                {
                    _logger.LogWarning("Erro ao adicionar o contato WhatsApp.");
                    return false;
                }

                _logger.LogInformation("Contato WhatsApp adicionado com sucesso.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao adicionar o contato WhatsApp.");
                return false;
            }
        }
    }
}
