using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.InputPort.Models;
using ContentGenerator.Api.Core.Models;
using ContentGenerator.Api.Core.OutputPort.PostPort;
using ContentGenerator.Api.Core.UseCases.PublicationCase.Interfaces;
using Microsoft.Extensions.Logging;

namespace ContentGenerator.Api.Core.UseCases.PublicationCase
{
    public class SearchPostPaged : ISearchPostPaged
    {
        private readonly IPostRepository _postRepository;
        private readonly ILogger<SearchPostPaged> _logger;

        public SearchPostPaged(IPostRepository postRepository, ILogger<SearchPostPaged> logger)
        {
            _postRepository = postRepository;
            _logger = logger;
        }

        public async Task<PageModel<SearchPostOutput>> SearchPaged(PaginationInput input)
        {
            _logger.LogInformation("Iniciando a busca paginada de publicações.");

            try
            {
                var posts = await _postRepository.GetPublicationsPaged(input);

                var page = new PageModel<SearchPostOutput>(
                    itemsPerPage: input.ItemsPerPage,
                    numberPage: input.PageNumber,
                    totalItems: posts.Any() ? posts.First().TotalCount : 0,
                    sortOrder: input.SortOrder,
                    sortColumn: input.SortColumn)
                {
                    Items = posts.ToList(),
                    TotalRecords = posts.Count()
                };

                _logger.LogInformation("Busca paginada de publicações concluída com sucesso.");
                return page;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao realizar a busca paginada de publicações.");
                return default!;
            }
        }
    }

}
