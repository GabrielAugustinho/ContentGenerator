using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.Entities;
using ContentGenerator.Api.Core.OutputPort.SubjectTypePort;
using ContentGenerator.Api.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace ContentGenerator.Api.Adapters.Repository
{
    public class SubjectTypeRepository : ISubjectTypeRepository
    {
        private readonly DataContext _dataContext;

        public SubjectTypeRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<SearchSubjectTypeOutput>?> GetAllSubjectType()
        {
            List<TipoAssunto> tipoAssuntos = await _dataContext.TipoAssunto.ToListAsync();

            return ToListEventTypeOutput(tipoAssuntos);
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
