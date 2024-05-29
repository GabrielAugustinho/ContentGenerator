using ContentGenerator.Api.Core.InputPort.EmailPort;
using ContentGenerator.Api.Core.OutputPort.EmailPort;

namespace ContentGenerator.Api.Core.Abstractions
{
    public interface IEmailRepository
    {
        Task<SearchEmailOutput?> GetEmailById(int id);
        Task<IEnumerable<SearchEmailOutput>> GetEmailPaged(SearchEmailInput input);
        Task<bool> DeleteEmail(int id);
        Task<bool> UpdateEmail(UpdateEmailInput input);
        Task<bool> AddEmail(AddEmailInput input);
    }
}
