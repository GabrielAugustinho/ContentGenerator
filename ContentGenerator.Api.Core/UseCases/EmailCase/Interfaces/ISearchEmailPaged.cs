using ContentGenerator.Api.Core.InputPort.EmailPort;
using ContentGenerator.Api.Core.Models;
using ContentGenerator.Api.Core.OutputPort.EmailPort;

namespace ContentGenerator.Api.Core.UseCases.EmailCase.Interfaces
{
    public interface ISearchEmailPaged
    {
        Task<PageModel<SearchEmailOutput>> SearchPaged(SearchEmailInput input);
        Task<SearchEmailOutput?> SearchById(int id);
    }
}
