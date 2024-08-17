using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.UseCases.EmailCase.Interfaces;
using Microsoft.Extensions.Logging;

namespace ContentGenerator.Api.Core.UseCases.EmailCase
{
    public class DeleteEmail : IDeleteEmail
    {
        private readonly IEmailRepository _emailRepository;
        private readonly ILogger<DeleteEmail> _logger;

        public DeleteEmail(IEmailRepository emailRepository, ILogger<DeleteEmail> logger)
        {
            _emailRepository = emailRepository;
            _logger = logger;
        }

        public async Task<bool> Execute(int id)
        {
            _logger.LogInformation("Deletando email com ID: {Id}", id);

            try
            {
                var result = await _emailRepository.DeleteEmail(id);
                if (result)
                {
                    _logger.LogInformation("Email com ID {Id} deletado com sucesso.", id);
                }
                else
                {
                    _logger.LogWarning("Erro ao deletar email com ID {Id}.", id);
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao deletar email com ID {Id}.", id);
                return false;
            }
        }
    }
}
