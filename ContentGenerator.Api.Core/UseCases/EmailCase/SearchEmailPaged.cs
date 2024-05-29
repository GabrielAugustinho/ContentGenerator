using ContentGenerator.Api.Core.Abstractions;
using ContentGenerator.Api.Core.InputPort.EmailPort;
using ContentGenerator.Api.Core.Models;
using ContentGenerator.Api.Core.OutputPort.EmailPort;
using ContentGenerator.Api.Core.UseCases.EmailCase.Interfaces;

namespace ContentGenerator.Api.Core.UseCases.EmailCase
{
    public class SearchEmailPaged : ISearchEmailPaged
    {
        private readonly IEmailRepository _emailRepository;

        public SearchEmailPaged(IEmailRepository emailRepository)
        {
            _emailRepository = emailRepository;
        }

        public async Task<SearchEmailOutput?> SearchById(int id)
        {
            try
            {
                var email = await _emailRepository.GetEmailById(id);
                return email;
            }
            catch
            {
                return default!;
            }
        }

        public async Task<PageModel<SearchEmailOutput>> SearchPaged(SearchEmailInput input)
        {
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

                return page;
            }
            catch
            {
                return default!;
            }
        }
    }
}
