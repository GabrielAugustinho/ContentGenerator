using ContentGenerator.Api.Core.InputPort.Models;
using ContentGenerator.Api.Core.Models;
using ContentGenerator.Api.Core.OutputPort.WhatsAppPort;

namespace ContentGenerator.Api.Core.UseCases.WhatsAppCase.Interfaces
{
    public interface ISearchWhatsAppPaged
    {
        Task<PageModel<SearchWhatsAppOutput>> SearchPaged(PaginationInput input);
        Task<SearchWhatsAppOutput?> SearchById(int id);
    }
}
