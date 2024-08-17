using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.Entities;
using ContentGenerator.Api.Core.OutputPort.SubjectTypePort;
using ContentGenerator.Api.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ContentGenerator.Api.Adapters.Repository
{
    public class SubjectTypeRepository : ISubjectTypeRepository
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<SubjectTypeRepository> _logger;

        public SubjectTypeRepository(DataContext dataContext, ILogger<SubjectTypeRepository> logger)
        {
            _dataContext = dataContext;
            _logger = logger;
        }

        public async Task<IEnumerable<SearchSubjectTypeOutput>?> GetAllSubjectType()
        {
            _logger.LogInformation("Buscando todos os tipos de assunto.");

            try
            {
                List<TipoAssunto> tipoAssuntos = await _dataContext.TipoAssunto.ToListAsync();
                _logger.LogInformation("Busca por todos os tipos de assunto concluída com sucesso.");
                return ToListEventTypeOutput(tipoAssuntos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar todos os tipos de assunto.");
                return null;
            }
        }

        private static List<SearchSubjectTypeOutput> ToListEventTypeOutput(List<TipoAssunto> tipoAssuntos)
        {
            var output = new List<SearchSubjectTypeOutput>();

            foreach (var item in tipoAssuntos)
                output.Add(new SearchSubjectTypeOutput(item.TipoAssuntoId, item.Assunto));

            return output;
        }
    }
}
