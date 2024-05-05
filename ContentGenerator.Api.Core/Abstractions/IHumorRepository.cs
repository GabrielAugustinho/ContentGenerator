using ContentGenerator.Api.Core.OutputPort.HumorPort;

namespace ContentGenerator.Api.Core.Abstractions
{
    public interface IHumorRepository
    {
        Task<IEnumerable<HumorOutput>?> GetAllHumor();
    }
}
