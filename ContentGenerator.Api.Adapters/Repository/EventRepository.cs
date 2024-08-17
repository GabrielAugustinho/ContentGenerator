using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.Entities;
using ContentGenerator.Api.Core.InputPort.EventPort;
using ContentGenerator.Api.Core.OutputPort.DestinyPort;
using ContentGenerator.Api.Core.OutputPort.EventPort;
using ContentGenerator.Api.Core.OutputPort.HumorPort;
using ContentGenerator.Api.Core.OutputPort.ValidationPort;
using ContentGenerator.Api.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ContentGenerator.Api.Adapters.Repository
{
    public class EventRepository : IEventRepository
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<EventRepository> _logger;

        public EventRepository(DataContext dataContext, ILogger<EventRepository> logger)
        {
            _dataContext = dataContext;
            _logger = logger;
        }

        public async Task<bool> AddEvent(AddEventInput input)
        {
            _logger.LogInformation("Adicionando novo evento.");

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

                _logger.LogInformation("Evento adicionado com sucesso.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao adicionar evento.");
                return false;
            }
        }

        public async Task<bool> DeleteEvent(int id)
        {
            _logger.LogInformation("Deletando evento com ID: {Id}", id);

            try
            {
                var homenagem = await _dataContext.Homenagem.FindAsync(id);

                if (homenagem is null)
                {
                    _logger.LogWarning("Evento com ID {Id} não encontrado.", id);
                    return false;
                }

                homenagem.Delete();
                var result = await _dataContext.SaveChangesAsync();

                if (result > 0)
                {
                    _logger.LogInformation("Evento com ID {Id} deletado com sucesso.", id);
                    return true;
                }
                else
                {
                    _logger.LogWarning("Falha ao deletar evento com ID {Id}.", id);
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao deletar evento com ID {Id}.", id);
                return false;
            }
        }

        public async Task<IEnumerable<SearchEventOutput>?> GetEventsByDate(SearchEventInput inputPort)
        {
            _logger.LogInformation("Buscando eventos na data: {Date}", inputPort.DateTime);

            try
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

                _logger.LogInformation("Busca por eventos na data {Date} concluída com sucesso.", inputPort.DateTime);
                return homenagens.Select(ToSearchEventOutput).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar eventos na data: {Date}", inputPort.DateTime);
                return null;
            }
        }

        public async Task<IEnumerable<SearchEventOutput>?> GetEventsOfMonth()
        {
            _logger.LogInformation("Buscando eventos do mês atual.");

            try
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

                _logger.LogInformation("Busca por eventos do mês atual concluída com sucesso.");
                return homenagens.Select(ToSearchEventOutput).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar eventos do mês atual.");
                return null;
            }
        }

        public async Task<bool> UpdateEvent(UpdateEventInput input)
        {
            _logger.LogInformation("Atualizando evento com ID: {Id}", input.EventoId);

            try
            {
                var existingHomenagem = await _dataContext.Homenagem
                    .FirstOrDefaultAsync(h => h.HomenagemId == input.EventoId);

                if (existingHomenagem == null)
                {
                    _logger.LogWarning("Evento com ID {Id} não encontrado.", input.EventoId);
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

                _logger.LogInformation("Evento com ID {Id} atualizado com sucesso.", input.EventoId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar evento com ID {Id}.", input.EventoId);
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
