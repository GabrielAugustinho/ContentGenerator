using ContentGenerator.Api.Core.InputPort.Models;
using ContentGenerator.Api.Core.InputPort.PublicationPort;
using ContentGenerator.Api.Core.OutputPort.PostPort;

namespace ContentGenerator.Api.Core.Abstractions
{
    public interface IPostRepository
    {
        Task<int?> AddPost(PostInput input);
        Task<IEnumerable<SearchPostOutput>> GetPublicationsPaged(PaginationInput input);
    }
}
