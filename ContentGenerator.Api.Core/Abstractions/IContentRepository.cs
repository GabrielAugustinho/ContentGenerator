using ContentGenerator.Api.Core.Entities;
using ContentGenerator.Api.Core.InputPort.ContentPort;

namespace ContentGenerator.Api.Core.Abstractions
{
    public interface IContentRepository
    {
        Task<int?> AddContent(ContentInput input);
        Task<Assunto?> GetContentById(int id);
        Task<IEnumerable<Assunto?>> GetContentOfMonth();
        Task<bool> UpdateContent(UpdateContentInput input);
    }
}
