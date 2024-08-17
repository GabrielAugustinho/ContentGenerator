using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.OutputPort.ValidationPort;
using Microsoft.Extensions.Logging;

namespace ContentGenerator.Api.Core.UseCases.ValidationCase
{
    public class SearchValidation : ISearchValidation
    {
        private readonly IValidationRepository _validationRepository;
        private readonly ILogger<SearchValidation> _logger;

        public SearchValidation(IValidationRepository validationRepository, ILogger<SearchValidation> logger)
        {
            _validationRepository = validationRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<SearchValidationOutput>?> Execute()
        {
            _logger.LogInformation("Iniciando a busca por todos os tipos de validação.");

            try
            {
                var outputList = await _validationRepository.GetAllValidationType();
                _logger.LogInformation("Busca por todos os tipos de validação concluída com sucesso.");
                return outputList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar todos os tipos de validação.");
                return null;
            }
        }
    }
}
