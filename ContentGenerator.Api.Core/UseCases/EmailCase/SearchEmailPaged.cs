using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.InputPort.EmailPort;
using ContentGenerator.Api.Core.Models;
using ContentGenerator.Api.Core.OutputPort.EmailPort;
using ContentGenerator.Api.Core.UseCases.EmailCase.Interfaces;
using Microsoft.Extensions.Logging;

namespace ContentGenerator.Api.Core.UseCases.EmailCase
{
    public class SearchEmailPaged : ISearchEmailPaged
    {
        private readonly IEmailRepository _emailRepository;
        private readonly ILogger<SearchEmailPaged> _logger;

        public SearchEmailPaged(IEmailRepository emailRepository, ILogger<SearchEmailPaged> logger)
        {
            _emailRepository = emailRepository;
            _logger = logger;
        }

        public async Task<SearchEmailOutput?> SearchById(int id)
        {
            _logger.LogInformation("Buscando email por ID: {Id}", id);

            try
            {
                var email = await _emailRepository.GetEmailById(id);
                _logger.LogInformation("Busca por email com ID {Id} concluída.", id);
                return email;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar email com ID {Id}.", id);
                return default!;
            }
        }

        public async Task<PageModel<SearchEmailOutput>> SearchPaged(SearchEmailInput input)
        {
            _logger.LogInformation("Iniciando busca paginada de emails.");

            try
            {
                var userOutputList = await _emailRepository.GetEmailPaged(input);

                var page = new PageModel<SearchEmailOutput>(itemsPerPage: input.Pagination.ItemsPerPage,
                                                            numberPage: input.Pagination.PageNumber,
                                                            totalItems: userOutputList.Any() ? userOutputList.First().TotalCount : 0,
                                                            sortOrder: input.Pagination.SortOrder,
                                                            sortColumn: input.Pagination.SortColumn)
                {
                    Items = userOutputList,
                    TotalRecords = userOutputList.Count()
                };

                _logger.LogInformation("Busca paginada de emails concluída com sucesso.");
                return page;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao realizar busca paginada de emails.");
                return default!;
            }
        }
    }
}
