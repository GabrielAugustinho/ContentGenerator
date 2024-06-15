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

        public async Task<bool> AddEvent(AddEventInput input)
        {
            try
            {
                var newHomenagem = new Homenagem
                {
                    DestinosId = input.DestinosId,
                    HumorId = input.HumorId,
                    TipoValidacaoId = input.TipoValidacaoId,
                    TipoHomenagemId = input.TipoEventoId,
                    Dia = input.Dia,
                    Mes = input.Mes,
                    Ano = input.Ano,
                    ObjEveAssunto = input.DescricaoUsuario,
                    Descricao = input.Descricao,
                    Ativo = true
                };

                await _dataContext.Homenagem.AddAsync(newHomenagem);
                await _dataContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteEvent(int id)
        {
            Homenagem? homenagem = await _dataContext.Homenagem.FindAsync(id);

            if (homenagem is null)
                return false;

            homenagem.Delete();

            var result = await _dataContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<IEnumerable<SearchEventOutput>?> GetEventsByDate(SearchEventInput inputPort)
        {
            var targetDate = inputPort.DateTime;
            var day = targetDate.Day;
            var month = targetDate.Month;
            var year = targetDate.Year;

            var homenagens = await _dataContext.Homenagem
                .Where(h => h.Ativo &&
                            ((h.Dia == day && h.Mes == month && h.Ano == year) ||
                             (h.Dia == day && h.Mes == month && h.TipoHomenagemId == 3)))
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

            return homenagens.Select(ToSearchEventOutput).ToList();
        }

        public async Task<bool> UpdateEvent(UpdateEventInput input)
        {
            try
            {
                var existingHomenagem = await _dataContext.Homenagem
                    .FirstOrDefaultAsync(h => h.HomenagemId == input.EventoId);

                if (existingHomenagem == null)
                {
                    return false;
                }

                existingHomenagem.DestinosId = input.DestinosId;
                existingHomenagem.HumorId = input.HumorId;
                existingHomenagem.TipoValidacaoId = input.TipoValidacaoId;
                existingHomenagem.TipoHomenagemId = input.TipoEventoId;
                existingHomenagem.Dia = input.Dia;
                existingHomenagem.Mes = input.Mes;
                existingHomenagem.Ano = input.Ano;
                existingHomenagem.ObjEveAssunto = input.DescricaoUsuario;
                existingHomenagem.Descricao = input.Descricao;

                _dataContext.Homenagem.Update(existingHomenagem);
                await _dataContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
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
