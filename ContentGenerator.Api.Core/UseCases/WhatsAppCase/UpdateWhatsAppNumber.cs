using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.InputPort.WhatsAppPort;
using ContentGenerator.Api.Core.UseCases.WhatsAppCase.Interfaces;
using Microsoft.Extensions.Logging;

namespace ContentGenerator.Api.Core.UseCases.WhatsAppCase
{
    public class UpdateWhatsAppNumber : IUpdateWhatsAppNumber
    {
        private readonly IWhatsAppRepository _whatsAppRepository;
        private readonly ILogger<UpdateWhatsAppNumber> _logger;

        public UpdateWhatsAppNumber(IWhatsAppRepository whatsAppRepository, ILogger<UpdateWhatsAppNumber> logger)
        {
            _whatsAppRepository = whatsAppRepository;
            _logger = logger;
        }

        public async Task<bool> Execute(UpdateWhatsAppInput input)
        {
            _logger.LogInformation("Iniciando a atualização do contato WhatsApp.");

            try
            {
                var result = await _whatsAppRepository.UpdateWhatsAppNumber(input);

                if (!result)
                {
                    _logger.LogWarning($"Erro ao atualizar o contato WhatsApp com ID {input.Id}.");
                    return false;
                }

                _logger.LogInformation($"Contato WhatsApp com ID {input.Id} atualizado com sucesso.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar o contato WhatsApp.");
                return false;
            }
        }
    }
}
