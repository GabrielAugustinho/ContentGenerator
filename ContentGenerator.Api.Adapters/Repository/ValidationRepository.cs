using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.Entities;
using ContentGenerator.Api.Core.OutputPort.ValidationPort;
using ContentGenerator.Api.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace ContentGenerator.Api.Adapters.Repository
{
    public class ValidationRepository : IValidationRepository
    {
        private readonly DataContext _dataContext;

        public ValidationRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<SearchValidationOutput>?> GetAllValidationType()
        {
            List<TipoValidacao> tipoValidacoes = await _dataContext.TipoValidacao.ToListAsync();

            return ToListValidationOutput(tipoValidacoes);
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
