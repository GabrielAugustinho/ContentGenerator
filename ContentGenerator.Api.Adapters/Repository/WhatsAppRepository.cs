using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.Entities;
using ContentGenerator.Api.Core.InputPort.WhatsAppPort;
using ContentGenerator.Api.Core.OutputPort.WhatsAppPort;
using ContentGenerator.Api.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace ContentGenerator.Api.Adapters.Repository.WhatsAppRepo
{
    public class WhatsAppRepository : IWhatsAppRepository
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<WhatsAppRepository> _logger;

        public WhatsAppRepository(DataContext dataContext, ILogger<WhatsAppRepository> logger)
        {
            _dataContext = dataContext;
            _logger = logger;
        }

        public async Task<SearchWhatsAppOutput?> GetWhatsAppById(int id)
        {
            _logger.LogInformation($"Buscando contato WhatsApp com ID {id}.");

            try
            {
                WhatsApp? whatsApp = await _dataContext.WhatsApp.FindAsync(id);

                if (whatsApp is null)
                {
                    _logger.LogWarning($"Contato WhatsApp com ID {id} não encontrado.");
                    return default;
                }

                _logger.LogInformation($"Contato WhatsApp com ID {id} encontrado com sucesso.");
                return WhatsAppToOutput(whatsApp);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao buscar o contato WhatsApp com ID {id}.");
                return default;
            }
        }

        public async Task<IEnumerable<SearchWhatsAppOutput>> GetWhatsAppPaged(SearchWhatsAppInput input)
        {
            _logger.LogInformation("Iniciando a busca paginada dos contatos WhatsApp.");

            try
            {
                int startIndex = (input.Pagination.PageNumber - 1) * input.Pagination.ItemsPerPage ?? 0;
                IQueryable<WhatsApp> query = _dataContext.WhatsApp;

                if (input.Active is not null)
                    query = query.Where(x => x.Ativo == input.Active);

                int totalCount = await query.CountAsync();

                if (!string.IsNullOrEmpty(input.Pagination.SortColumn))
                {
                    var parameter = Expression.Parameter(typeof(WhatsApp), "x");
                    var property = Expression.Property(parameter, input.Pagination.SortColumn);
                    var lambda = Expression.Lambda(property, parameter);

                    query = input.Pagination.SortOrder.ToLower() switch
                    {
                        "asc" => Queryable.OrderBy(query, (dynamic)lambda),
                        "desc" => Queryable.OrderByDescending(query, (dynamic)lambda),
                        _ => query
                    };
                }

                query = query.Skip(startIndex).Take(input.Pagination.ItemsPerPage);
                List<WhatsApp> whatsApps = await query.ToListAsync();
                _logger.LogInformation("Busca paginada dos contatos WhatsApp concluída com sucesso.");

                return ListWhatsAppToListOutput(whatsApps, totalCount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao realizar a busca paginada dos contatos WhatsApp.");
                return Enumerable.Empty<SearchWhatsAppOutput>();
            }
        }

        private static List<SearchWhatsAppOutput> ListWhatsAppToListOutput(List<WhatsApp> whatsApp, int totalCount)
        {
            var output = new List<SearchWhatsAppOutput>();

            foreach (var item in whatsApp)
                output.Add(new SearchWhatsAppOutput(item.WhatsAppId, item.Nome, item.Numero_Fone, item.Ativo, totalCount));

            return output;
        }

        private static SearchWhatsAppOutput WhatsAppToOutput(WhatsApp whatsApp)
        {
            var output = new SearchWhatsAppOutput(whatsApp.WhatsAppId, whatsApp.Nome, whatsApp.Numero_Fone, whatsApp.Ativo, 1);
            return output;
        }

        public async Task<bool> AddWhatsAppNumber(AddWhatsAppInput input)
        {
            _logger.LogInformation("Iniciando a adição do contato WhatsApp.");

            try
            {
                var whatsApp = new WhatsApp() { Nome = input.Nome, Numero_Fone = input.NumeroFone };

                _dataContext.WhatsApp.Add(whatsApp);
                var result = await _dataContext.SaveChangesAsync();

                if (result <= 0)
                {
                    _logger.LogWarning("Erro ao adicionar o contato WhatsApp.");
                    return false;
                }

                _logger.LogInformation("Contato WhatsApp adicionado com sucesso.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao adicionar o contato WhatsApp.");
                return false;
            }
        }

        public async Task<bool> UpdateWhatsAppNumber(UpdateWhatsAppInput input)
        {
            _logger.LogInformation($"Iniciando a atualização do contato WhatsApp com ID {input.Id}.");

            try
            {
                WhatsApp? dbWhatsApp = await _dataContext.WhatsApp.FindAsync(input.Id);

                if (dbWhatsApp is null)
                {
                    _logger.LogWarning($"Contato WhatsApp com ID {input.Id} não encontrado para atualização.");
                    return false;
                }

                dbWhatsApp.Update(input.Nome, input.NumeroFone);

                var result = await _dataContext.SaveChangesAsync();
                if (result <= 0)
                {
                    _logger.LogWarning($"Erro ao atualizar o contato WhatsApp com ID {input.Id}.");
                    return false;
                }

                _logger.LogInformation($"Contato WhatsApp com ID {input.Id} atualizado com sucesso.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao atualizar o contato WhatsApp com ID {input.Id}.");
                return false;
            }
        }

        public async Task<bool> DeleteWhatsAppNumber(int id)
        {
            _logger.LogInformation($"Iniciando a exclusão do contato WhatsApp com ID {id}.");

            try
            {
                WhatsApp? dbWhatsApp = await _dataContext.WhatsApp.FindAsync(id);

                if (dbWhatsApp is null)
                {
                    _logger.LogWarning($"Contato WhatsApp com ID {id} não encontrado para exclusão.");
                    return false;
                }

                dbWhatsApp.Delete();

                var result = await _dataContext.SaveChangesAsync();
                if (result <= 0)
                {
                    _logger.LogWarning($"Erro ao deletar o contato WhatsApp com ID {id}.");
                    return false;
                }

                _logger.LogInformation($"Contato WhatsApp com ID {id} deletado com sucesso.");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao deletar o contato WhatsApp com ID {id}.");
                return false;
            }
        }
    }
}
