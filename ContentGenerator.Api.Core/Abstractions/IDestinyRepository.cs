using ContentGenerator.Api.Core.OutputPort.DestinyPort;

namespace ContentGenerator.Api.Core.Abstractions
{
    public interface IDestinyRepository
    {
        Task<IEnumerable<SearchDestinyOutput>> GetAllDestiny();
    }
}
