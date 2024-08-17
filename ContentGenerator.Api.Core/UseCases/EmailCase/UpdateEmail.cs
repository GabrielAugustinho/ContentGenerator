using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.InputPort.EmailPort;
using ContentGenerator.Api.Core.UseCases.EmailCase.Interfaces;
using Microsoft.Extensions.Logging;

namespace ContentGenerator.Api.Core.UseCases.EmailCase
{
    public class UpdateEmail : IUpdateEmail
    {
        private readonly IEmailRepository _emailRepository;
        private readonly ILogger<UpdateEmail> _logger;

        public UpdateEmail(IEmailRepository emailRepository, ILogger<UpdateEmail> logger)
        {
            _emailRepository = emailRepository;
            _logger = logger;
        }

        public async Task<bool> Execute(UpdateEmailInput input)
        {
            _logger.LogInformation("Atualizando email com ID: {Id}", input.Id);

            try
            {
                var result = await _emailRepository.UpdateEmail(input);

                if (result)
                {
                    _logger.LogInformation("Email com ID {Id} atualizado com sucesso.", input.Id);
                }
                else
                {
                    _logger.LogWarning("Erro ao atualizar email com ID {Id}.", input.Id);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar email com ID {Id}.", input.Id);
                return false;
            }
        }
    }
}
