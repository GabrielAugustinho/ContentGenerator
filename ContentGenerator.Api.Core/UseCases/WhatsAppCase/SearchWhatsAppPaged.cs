using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.InputPort.WhatsAppPort;
using ContentGenerator.Api.Core.Models;
using ContentGenerator.Api.Core.OutputPort.WhatsAppPort;
using ContentGenerator.Api.Core.UseCases.WhatsAppCase.Interfaces;
using Microsoft.Extensions.Logging;

namespace ContentGenerator.Api.Core.UseCases.WhatsAppCase
{
    public class SearchWhatsAppPaged : ISearchWhatsAppPaged
    {
        private readonly IWhatsAppRepository _whatsAppRepository;
        private readonly ILogger<SearchWhatsAppPaged> _logger;

        public SearchWhatsAppPaged(IWhatsAppRepository whatsAppRepository, ILogger<SearchWhatsAppPaged> logger)
        {
            _whatsAppRepository = whatsAppRepository;
            _logger = logger;
        }

        public async Task<PageModel<SearchWhatsAppOutput>> SearchPaged(SearchWhatsAppInput input)
        {
            _logger.LogInformation("Iniciando a busca paginada de contatos WhatsApp.");

            try
            {
                var userOutputList = await _whatsAppRepository.GetWhatsAppPaged(input);

                var page = new PageModel<SearchWhatsAppOutput>(itemsPerPage: input.Pagination.ItemsPerPage,
                                                               numberPage: input.Pagination.PageNumber,
                                                               totalItems: userOutputList.Any() ? userOutputList.First().TotalCount : 0,
                                                               sortOrder: input.Pagination.SortOrder,
                                                               sortColumn: input.Pagination.SortColumn)
                {
                    Items = userOutputList,
                    TotalRecords = userOutputList.Count()
                };

                _logger.LogInformation("Busca paginada de contatos WhatsApp concluída com sucesso.");
                return page;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao realizar a busca paginada de contatos WhatsApp.");
                return default!;
            }
        }

        public async Task<SearchWhatsAppOutput?> SearchById(int id)
        {
            _logger.LogInformation($"Iniciando a busca do contato WhatsApp com ID {id}.");

            try
            {
                var whatsAppOutput = await _whatsAppRepository.GetWhatsAppById(id);
                if (whatsAppOutput == null)
                    _logger.LogWarning($"Contato WhatsApp com ID {id} não encontrado.");

                _logger.LogInformation($"Busca do contato WhatsApp com ID {id} concluída com sucesso.");
                return whatsAppOutput;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao buscar o contato WhatsApp com ID {id}.");
                return default!;
            }
        }
    }
}
