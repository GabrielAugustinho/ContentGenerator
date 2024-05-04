using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.InputPort.WhatsAppPort;
using ContentGenerator.Api.Core.Models;
using ContentGenerator.Api.Core.OutputPort.WhatsAppPort;
using ContentGenerator.Api.Core.UseCases.WhatsAppCase.Interfaces;

namespace ContentGenerator.Api.Core.UseCases.WhatsAppCase
{
    public class SearchWhatsAppPaged : ISearchWhatsAppPaged
    {
        private readonly IWhatsAppRepository _whatsAppRepository;

        public SearchWhatsAppPaged(IWhatsAppRepository whatsAppRepository)
        {
            _whatsAppRepository = whatsAppRepository;
        }

        public async Task<PageModel<SearchWhatsAppOutput>> SearchPaged(SearchWhatsAppInput input)
        {
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

                return page;
            }
            catch
            {
                return default!;
            }
        }

        public async Task<SearchWhatsAppOutput?> SearchById(int id)
        {
            try
            {
                var whatsAppOutput = await _whatsAppRepository.GetWhatsAppById(id);
                return whatsAppOutput;
            }
            catch
            {
                return default!;
            }
        }
    }
}
