using ContentGenerator.Api.Core.InputPort.Models;
using ContentGenerator.Api.Core.InputPort.WhatsAppPort;
using ContentGenerator.Api.Core.OutputPort.WhatsAppPort;

namespace ContentGenerator.Api.Core.Abstractions
{
    public interface IWhatsAppRepository
    {
        Task<SearchWhatsAppOutput?> GetWhatsAppById(int id);
        Task<IEnumerable<SearchWhatsAppOutput>> GetWhatsAppPaged(PaginationInput input);
        Task<bool> AddWhatsAppNumber(AddWhatsAppInput input);
        Task<bool> UpdateWhatsAppNumber(UpdateWhatsAppInput input);
        Task<bool> DeleteWhatsAppNumber(int id);
    }
}
