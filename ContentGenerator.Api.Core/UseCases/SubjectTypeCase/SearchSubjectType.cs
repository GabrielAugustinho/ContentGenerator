using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.OutputPort.SubjectTypePort;
using Microsoft.Extensions.Logging;

namespace ContentGenerator.Api.Core.UseCases.SubjectTypeCase
{
    public class SearchSubjectType : ISearchSubjectType
    {
        private readonly ISubjectTypeRepository _subjectTypeRepository;
        private readonly ILogger<SearchSubjectType> _logger;

        public SearchSubjectType(ISubjectTypeRepository subjectTypeRepository, ILogger<SearchSubjectType> logger)
        {
            _subjectTypeRepository = subjectTypeRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<SearchSubjectTypeOutput>?> Execute()
        {
            _logger.LogInformation("Iniciando a busca por todos os tipos de assunto.");

            try
            {
                var outputList = await _subjectTypeRepository.GetAllSubjectType();
                _logger.LogInformation("Busca por todos os tipos de assunto concluída com sucesso.");
                return outputList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar todos os tipos de assunto.");
                return null;
            }
        }
    }
}
