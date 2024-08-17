using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.InputPort.EmailPort;
using ContentGenerator.Api.Core.UseCases.EmailCase.Interfaces;
using Microsoft.Extensions.Logging;

namespace ContentGenerator.Api.Core.UseCases.EmailCase
{
    public class AddEmail : IAddEmail
    {
        private readonly IEmailRepository _emailRepository;
        private readonly ILogger<AddEmail> _logger;

        public AddEmail(IEmailRepository emailRepository, ILogger<AddEmail> logger)
        {
            _emailRepository = emailRepository;
            _logger = logger;
        }

        public async Task<bool> Execute(AddEmailInput input)
        {
            _logger.LogInformation("Adicionando novo email.");

            try
            {
                var result = await _emailRepository.AddEmail(input);
                if (result)
                {
                    _logger.LogInformation("Email adicionado com sucesso.");
                }
                else
                {
                    _logger.LogWarning("Erro ao adicionar email.");
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao adicionar email.");
                return false;
            }
        }
    }
}
