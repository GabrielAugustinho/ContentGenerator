using ContentGenerator.Api.Core.InputPort.Models;
using ContentGenerator.Api.Core.Models;
using ContentGenerator.Api.Core.OutputPort.PostPort;

namespace ContentGenerator.Api.Core.UseCases.PublicationCase.Interfaces
{
    public interface ISearchPostPaged
    {
        Task<PageModel<SearchPostOutput>> SearchPaged(PaginationInput input);
    }
}
