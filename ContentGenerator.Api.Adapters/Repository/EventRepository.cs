using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.Entities;
using ContentGenerator.Api.Core.InputPort.EventPort;
using ContentGenerator.Api.Core.OutputPort.DestinyPort;
using ContentGenerator.Api.Core.OutputPort.EventPort;
using ContentGenerator.Api.Core.OutputPort.HumorPort;
using ContentGenerator.Api.Core.OutputPort.ValidationPort;
using ContentGenerator.Api.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace ContentGenerator.Api.Adapters.Repository
{
    public class EventRepository : IEventRepository
    {
        private readonly DataContext _dataContext;

        public EventRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<SearchEventOutput>?> GetEventsByDate(SearchEventInput inputPort)
        {
            var targetDate = inputPort.DateTime;
            var day = targetDate.Day;
            var month = targetDate.Month;
            var year = targetDate.Year;

            var homenagens = await _dataContext.Homenagem
                .Where(h => h.Ativo && h.Dia == day && h.Mes == month && h.Ano == year)
                .Include(h => h.Destinos)
                .Include(h => h.Humor)
                .Include(h => h.TipoValidacao)
                .Include(h => h.TipoHomenagem)
                .ToListAsync();

            return homenagens.Select(h => ToSearchEventOutput(h)).ToList();
        }

        public async Task<IEnumerable<SearchEventOutput>?> GetEventsOfMonth()
        {
            var currentMonth = DateTime.Now.Month;
            var currentYear = DateTime.Now.Year;

            var homenagens = await _dataContext.Homenagem
                .Where(h => h.Ativo && h.Mes == currentMonth && h.Ano == currentYear)
                .Include(h => h.Destinos)
                .Include(h => h.Humor)
                .Include(h => h.TipoValidacao)
                .Include(h => h.TipoHomenagem)
                .ToListAsync();

            return homenagens.Select(h => ToSearchEventOutput(h)).ToList();
        }

        private SearchEventOutput ToSearchEventOutput(Homenagem h)
        {
            return new SearchEventOutput(
                h.HomenagemId,
                new SearchDestinyOutput(h.Destinos.DestinosId, h.Destinos.Descricao),
                new SearchHumorOutput(h.Humor.HumorId, h.Humor.Descricao),
                new SearchValidationOutput(h.TipoValidacao.TipoValidacaoId, h.TipoValidacao.Tipo),
                new SearchEventTypeOutput(h.TipoHomenagem.TipoHomenagemId, h.TipoHomenagem.Descricao), 
                h.Dia,
                h.Mes,
                h.Ano,
                h.ObjEveAssunto,
                h.Descricao,
                h.Ativo
            );
        }
    }
}
