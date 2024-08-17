using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.Entities;
using ContentGenerator.Api.Core.OutputPort.ValidationPort;
using ContentGenerator.Api.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ContentGenerator.Api.Adapters.Repository
{
    public class ValidationRepository : IValidationRepository
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<ValidationRepository> _logger;

        public ValidationRepository(DataContext dataContext, ILogger<ValidationRepository> logger)
        {
            _dataContext = dataContext;
            _logger = logger;
        }

        public async Task<IEnumerable<SearchValidationOutput>?> GetAllValidationType()
        {
            _logger.LogInformation("Buscando todos os tipos de validação.");

            try
            {
                List<TipoValidacao> tipoValidacoes = await _dataContext.TipoValidacao.ToListAsync();
                _logger.LogInformation("Busca por todos os tipos de validação concluída com sucesso.");
                return ToListValidationOutput(tipoValidacoes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar todos os tipos de validação.");
                return null;
            }
        }

        private static List<SearchValidationOutput> ToListValidationOutput(List<TipoValidacao> tipoValidacoes)
        {
            var output = new List<SearchValidationOutput>();

            foreach (var item in tipoValidacoes)
                output.Add(new SearchValidationOutput(item.TipoValidacaoId, item.Tipo));

            return output;
        }
    }
}
