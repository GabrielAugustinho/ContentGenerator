using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.UseCases.WhatsAppCase.Interfaces;
using Microsoft.Extensions.Logging;

namespace ContentGenerator.Api.Core.UseCases.WhatsAppCase
{
    public class DeleteWhatsApp : IDeleteWhatsApp
    {
        private readonly IWhatsAppRepository _whatsAppRepository;
        private readonly ILogger<DeleteWhatsApp> _logger;

        public DeleteWhatsApp(IWhatsAppRepository whatsAppRepository, ILogger<DeleteWhatsApp> logger)
        {
            _whatsAppRepository = whatsAppRepository;
            _logger = logger;
        }

        public async Task<bool> Execute(int id)
        {
            _logger.LogInformation($"Iniciando a exclusão do contato WhatsApp com ID {id}.");

            try
            {
                var result = await _whatsAppRepository.DeleteWhatsAppNumber(id);

                if (!result)
                {
                    _logger.LogWarning($"Erro ao deletar o contato WhatsApp com ID {id}.");
                    return false;
                }

                _logger.LogInformation($"Contato WhatsApp com ID {id} deletado com sucesso.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao deletar o contato WhatsApp com ID {id}.");
                return false;
            }
        }
    }
}
